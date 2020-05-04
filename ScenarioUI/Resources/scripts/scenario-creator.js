function initScenarioPage(reflectedCollection, scenarioPage) {
    return new Vue({
        el: '#scenario-creator',
        data: {
            reflectedCollection: reflectedCollection,
            scenarioPage: scenarioPage,
            existingVariables: [],
            existingMethods: [],
            constantMethods: {},
            focusedActionIndex: null,
            actionTypes: {
                assign: 0,
                execute: 1,
                assert: 2
            },
            methodTypes: {
                constructor: 0,
                static: 1,
                method: 2
            }
        },
        computed: {
            orderedActions: function() {
                return _.orderBy(this.scenarioPage.Actions, 'Order');
            }
        },
        methods: {
            saveScenario: function() {
                axios.post('/scenario/creator', this.scenarioPage)
                    .then(function (response) {
                        var scenarioListUrl = window.location.origin + '/scenario';
                        window.open(scenarioListUrl, "_self");
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            createNewAction: function(type) {
                var newAction = {
                    Type: type,
                    Order: scenarioPage.Actions.length
                };

                // TODO: setup watchers (currently, doesn't watch after object created)
                switch (type) {
                    case this.actionTypes.assign:
                        newAction.Variable = {
                            Name: '',
                            ConstantValue: '',
                            Type: 'String'
                        };
                        break;
                    case this.actionTypes.execute:
                        newAction.Variable = {};
                        newAction.Method = {
                            Arguments: []
                        };
                        break;
                    case this.actionTypes.assert:
                        newAction.Assert = {
                            Type: -1
                        };
                        break;
                }

                scenarioPage.Actions.push(newAction);
            },
            recalculateExistingVariables: function () {
                var actions = this.scenarioPage.Actions;
                var actionTypes = this.actionTypes;

                var variables = actions.filter(function (action) {
                    return (action.Type === actionTypes.assign || action.Type === actionTypes.execute) &&
                        action.Variable &&
                        action.Variable.Name;
                }).map(function (action) {
                    return action.Variable;
                });
                
                var variablesNames = variables.map(function(variable) {
                    return variable.Name;
                });
                var existingVariables = variables.filter((v, i) => variablesNames.indexOf(v.Name) === i);
                this.existingVariables = existingVariables;

                return this.existingVariables;
            },
            recalculateExistingMethods: function () {
                var variableMethods = this.getVariableMethods();
                this.existingMethods = _.merge({}, this.constantMethods, variableMethods);
            },
            getParametersString: function(parameters) {
                return parameters.map(function (parameter) {
                    return parameter.Name;
                }).join(', ');
            },
            getConstantMethods: function () {
                var _this = this;
                var constantMethods = {};
                
                this.reflectedCollection.forEach(function(type) {
                    type.Constructors.forEach(function(constructor) {
                        var parameters = _this.getParametersString(constructor.Parameters);
                        var constructorName = constructor.Title ? constructor.Title : `new ${type.Type}(${parameters})`;
                        constantMethods[constructorName] = {
                            MethodType: _this.methodTypes.constructor,
                            TypeName: type.FullName,
                            Arguments: constructor.Parameters
                        }
                    });

                    var staticMethods = type.Methods.filter(function(method) {
                        return method.IsStatic;
                    });
                    staticMethods.forEach(function(static) {
                        var parameters = _this.getParametersString(static.Parameters);
                        var staticName = static.Title ? static.Title : `${type.Type}.${static.FullName}(${parameters})`;
                        constantMethods[staticName] = {
                            MethodType: _this.methodTypes.static,
                            Name: static.FullName,
                            TypeName: type.FullName,
                            Arguments: static.Parameters
                        }
                    });
                });

                return constantMethods;
            },
            getVariableMethods: function () {
                var _this = this;
                var variableMethods = {};
                var existingVariables = this.recalculateExistingVariables();

                var typeVariables = existingVariables.filter(function(variable) {
                    if (variable.Type)
                        return true;

                    return false;
                }).map(function(variable) {
                    return {
                        name: variable.Name,
                        type: variable.Type
                    }
                });

                if (typeVariables.length !== 0) {
                    this.reflectedCollection.forEach(function (reflectedType) {
                        typeVariables.forEach(function(typeVariable) {
                            if (typeVariable.type === reflectedType.FullName) {
                                var methods = reflectedType.Methods.filter(function (method) {
                                    return !method.IsStatic;
                                });
                                methods.forEach(function(method) {
                                    var parameters = _this.getParametersString(method.Parameters);
                                    var methodName = `${typeVariable.name}.` + (method.Title ? method.Title : `${method.FullName}(${parameters})`);
                                    variableMethods[methodName] = {
                                        MethodType: _this.methodTypes.method,
                                        Name: method.FullName,
                                        VariableName: typeVariable.name,
                                        Arguments: method.Parameters
                                    }
                                });
                            }
                        });
                    });
                }

                return variableMethods;
            },
            focusAction: function(index) {
                this.focusedActionIndex = index;
            },
            upAction: function(index) {
                if (index === 0)
                    return;

                var newOrder = this.scenarioPage.Actions[index - 1].Order;
                this.scenarioPage.Actions[index - 1].Order = this.scenarioPage.Actions[index].Order;
                this.scenarioPage.Actions[index].Order = newOrder;
            },
            downAction: function(index) {
                if (index === this.scenarioPage.Actions.length - 1)
                    return;

                var newOrder = this.scenarioPage.Actions[index + 1].Order;
                this.scenarioPage.Actions[index + 1].Order = this.scenarioPage.Actions[index].Order;
                this.scenarioPage.Actions[index].Order = newOrder;
            },
            removeAction: function (index) {
                for (var i = index + 1; i < this.scenarioPage.Actions.length; i++) {
                    this.scenarioPage.Actions[i].Order--;
                }
                this.scenarioPage.Actions.splice(index, 1);
            }
        },
        created: function () {
            this.constantMethods = this.getConstantMethods();
        }
    });
};

var Action = Vue.component('action',
    {
        props: ['action']
    });

Vue.component('assign',
    {
        extends: Action,
        template: '#assign-template',
        watch: {
            'action.Variable.ConstantValue': function(newVal, oldVal) {
                this.debouncedSetType(newVal);
            }
        },
        methods: {
            setType: function(value) {
                function getType() {
                    if (value === 'true' || value === 'false')
                        return 'Boolean';

                    if (/^\d+$/.test(value))
                        return 'Int32';

                    return 'String';
                }

                this.action.Variable.Type = getType();
            }
        },
        created: function () {
            this.debouncedSetType = _.debounce(this.setType, 500);
        }
    });

Vue.component('execute',
    {
        extends: Action,
        template: '#execute-template',
        data: function() {
            return {
                selectedMethod: this.getDefaultMethod(),
                argumentsPlaceholders: [],
                removingParentheses: false
            }
        },
        watch: {
            selectedMethod: function (newVal, oldVal) {
                if (this.removingParentheses) {
                    this.removingParentheses = false;
                    return;
                }

                this.debouncedSelectMethod(newVal);
            }
        },
        methods: {
            getDefaultMethod: function () {
                var method = this.action.Method;

                if (method.IsStatic)
                    return `${method.TypeName}.${method.Name}`;
                else if (method.IsConstructor)
                    return `new ${method.TypeName}`;
                else if (method.Variable)
                    return `${method.Variable.Name}.${method.Name}`;
                else
                    return '';
            },
            selectMethod: function (methodName) {
                var existingMethod = this.$root.existingMethods[methodName];
                this.action.Method = {
                    Arguments: []
                };
                this.action.Variable.Type = null;

                if (!existingMethod)
                    return;

                // remove parentheses
                var parenthesesIndex = methodName.indexOf('(');
                if (parenthesesIndex !== -1) {
                    this.removingParentheses = true;
                    this.selectedMethod = methodName.slice(0, parenthesesIndex);
                }

                var methodTypes = this.$root.methodTypes;
                switch (existingMethod.MethodType) {
                    case methodTypes.constructor:
                        this.action.Method.IsConstructor = true;
                        this.action.Method.IsStatic = false;
                        this.action.Method.TypeName = existingMethod.TypeName;
                        this.action.Method.Name = null;
                        this.action.Method.Variable = null;
                        this.action.Variable.Type = existingMethod.TypeName;
                        break;
                    case methodTypes.static:
                        this.action.Method.IsConstructor = false;
                        this.action.Method.IsStatic = true;
                        this.action.Method.TypeName = existingMethod.TypeName;
                        this.action.Method.Name = existingMethod.Name;
                        this.action.Method.Variable = null;
                        this.action.Variable.Type = null;
                        break;
                    case methodTypes.method:
                        this.action.Method.IsConstructor = false;
                        this.action.Method.IsStatic = false;
                        this.action.Method.TypeName = null;
                        this.action.Method.Name = existingMethod.Name;
                        this.action.Method.Variable = {
                            Name: existingMethod.VariableName
                        };
                        this.action.Variable.Type = null;
                        break;
                }

                // arguments
                for (var i = 0; i < existingMethod.Arguments.length; i++) {
                    var argument = existingMethod.Arguments[i];

                    this.action.Method.Arguments.push({
                        Order: argument.Position,
                        Variable: {}
                    });

                    this.argumentsPlaceholders.push(argument.Name);
                }
            }
        },
        created: function () {
            this.debouncedSelectMethod = _.debounce(this.selectMethod, 500);
        }
    });

Vue.component('assert',
    {
        extends: Action,
        template: '#assert-template',
        data: function() {
            return {
                assertTypes: [
                    { text: 'AreEqual', value: 0 },
                    { text: 'AreNotEqual', value: 1 },
                    { text: 'IsTrue', value: 2 },
                    { text: 'IsFalse', value: 3 }
                ]
            }
        },
        watch: {
            'action.Assert.Type': function (newVal, oldVal) {
                this.$set(this.action.Assert,
                    'ValueVariable',
                    {
                        Name: ''
                    });
                
                if (newVal === this.assertTypes[0].value || newVal === this.assertTypes[1].value) {
                    this.$set(this.action.Assert,
                        'ExpectedVariable',
                        {
                            Name: ''
                        });
                } else {
                    this.$set(this.action.Assert,
                        'ExpectedVariable',
                        null);
                }
            }
        }
    });

Vue.component('variable',
    {
        template: '#variable-template',
        props: {
            variable: Object,
            placeholder: String,
            includeComma: {
                type: Boolean,
                default: false
            },
            isReadonly: {
                type: Boolean,
                default: false
            }
        },
        data: function () {
            return {
                variableValue: this.getDefaultValue(),
                propertyValue: null,
                availableProperties: []
            }
        },
        watch: {
            variableValue: function(newVal, oldVal) {
                this.debouncedSetupValue(newVal);
            },
            propertyValue: function(newVal, oldVal) {
                this.setProperty(newVal);
            }
        },
        methods: {
            getDefaultValue: function() {
                if (this.variable.Name)
                    return this.variable.Name;

                if (this.variable.ConstantValue)
                    return this.variable.ConstantValue;

                return '';
            },
            setupValue: function() {
                var variableValue = this.variableValue;

                var existingVariable = this.getExistingVariable(variableValue);
                this.checkPropertiesAvailability(existingVariable);
                
                if (!existingVariable && this.isReadonly) {
                    this.setConstant(variableValue);
                } else {
                    this.setNewVariable(variableValue);
                }
            },
            setConstant: function (variableValue) {
                this.variable.Name = null;
                this.variable.Type = this.getType(variableValue);
                this.variable.ConstantValue = variableValue;
                this.variable.PropertyName = null;
            },
            setNewVariable: function (variableValue) {
                this.variable.Name = variableValue;
                this.variable.PropertyName = null;
                if (this.isReadonly) {
                    this.variable.Type = null;
                    this.variable.ConstantValue = null;
                }
            },
            setProperty: function (propertyValue) {
                this.variable.Name = this.variableValue;
                this.variable.Type = null;
                this.variable.ConstantValue = null;
                this.variable.PropertyName = propertyValue;
            },
            getExistingVariable: function (variableValue) {
                if (!variableValue)
                    return null;

                var existingVariable = this.$root.existingVariables.find(function (variable) {
                    return variable.Name === variableValue;
                });

                return existingVariable;
            },
            checkPropertiesAvailability: function (existingVariable) {
                if (!existingVariable) {
                    this.availableProperties = [];
                    this.variable.PropertyName = null;
                    return;
                }

                var reflectedType = this.$root.reflectedCollection.find(function (type) {
                    return type.FullName === existingVariable.Type;
                });

                if (!reflectedType) {
                    this.availableProperties = [];
                    this.variable.PropertyName = null;
                    return;
                }

                this.availableProperties = reflectedType.Properties.map(function(property) {
                    return property.FullName;
                });
            },
            getType: function(variableValue) {
                if (variableValue === 'true' || variableValue === 'false')
                    return 'Boolean';

                if (/^\d+$/.test(variableValue))
                    return 'Int32';

                return 'String';
            }
        },
        created: function () {
            this.debouncedSetupValue = _.debounce(this.setupValue, 500);
        }
    });