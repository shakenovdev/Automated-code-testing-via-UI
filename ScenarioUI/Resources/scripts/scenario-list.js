let EventBus = new Vue();

function initScenarioList(scenarioList) {
    return new Vue({
        el: '#scenario-list',
        data: {
            scenarioList: scenarioList,
            isExecuting: false,
            executingScenarioIds: [],
            executionResults: {}
        },
        methods: {
            removeScenario: function (index, collection, toTrash) {
                var _this = this;
                var trashScenario = collection[index];

                axios.post('/scenario/list/deleteScenario',
                    null,
                    {
                        params: {
                            scenarioId: trashScenario.Id
                        }
                    })
                    .then(function (response) {
                        collection.splice(index, 1);
                        if (toTrash) {
                            _this.scenarioList.TrashScenarios.push(trashScenario);
                        }
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            removeFolder: function (index, collection) {
                var _this = this;
                var trashFolder = collection[index];

                axios.post('/scenario/list/deleteFolder',
                        null,
                        {
                            params: {
                                folderId: trashFolder.Id
                            }
                        })
                    .then(function (response) {
                        var trashScenarios = response.data;
                        collection.splice(index, 1);
                        _this.scenarioList.TrashScenarios = trashScenarios;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            runAll: function () {
                var _this = this;
                _this.isExecuting = true;

                axios.post('/scenario/executor/runAll')
                    .then(function (response) {
                        _this.executingScenarioIds = response.data;
                        _this.executionResults = {};
                        EventBus.$emit("runAll");

                        var stopInterval = function (interval) {
                            clearInterval(interval);
                            _this.isExecuting = false;
                        };

                        var getAllResults = setInterval(function () {
                            axios.get('/scenario/executor/getAllExecutionResults',
                                    {
                                        params: {
                                            scenarioIds: _this.executingScenarioIds
                                        }
                                    })
                                .then(function (response) {
                                    var data = response.data;
                                    console.log(data);
                                    
                                    if (data) {
                                        for (var i = 0; i < data.length; i++) {
                                            var executingResult = data[i];

                                            var indexToDelete = _this.executingScenarioIds.indexOf(executingResult.ScenarioId);
                                            if (indexToDelete > -1)
                                                _this.executingScenarioIds.splice(indexToDelete, 1);

                                            _this.executionResults[executingResult.ScenarioId] = executingResult;
                                        }

                                        if (_this.executingScenarioIds.length === 0)
                                            stopInterval(getAllResults);
                                    }
                                })
                                .catch(function (error) {
                                    console.log(error);
                                    stopInterval(getAllResults);
                                });
                        }, 5000);
                    })
                    .catch(function(error) {
                        console.log(error);
                        _this.isExecuting = false;
                    });
            },
            getExecutingResult: function(scenarioId) {
                return this.executionResults[scenarioId];
            },
            createNewFolder: function () {
                var folders = this.scenarioList.Folders;

                axios.post('/scenario/list/createFolder',
                        {
                            Name: "new folder"
                        })
                    .then(function (response) {
                        var newFolder = response.data;
                        folders.push(newFolder);
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            openScenarioPage: function(scenarioId) {
                var scenarioPageUrl = window.location.origin + '/scenario/creator';

                if (scenarioId)
                    scenarioPageUrl += '?scenarioId=' + scenarioId;

                window.open(scenarioPageUrl, "_self");
            }
        }
    });
};

Vue.component('scenario',
    {
        template: "#scenario-template",
        props: ['scenario'],
        data: function () {
            return {
                isExecuting: false
            }
        },
        methods: {
            execute: function () {
                var _this = this;
                _this.isExecuting = true;
                var scenarioId = _this.scenario.Id;

                var checkResult = function () {
                    var stopInterval = function(interval) {
                        clearInterval(interval);
                        _this.isExecuting = false;
                    };

                    var interval = setInterval(function() {
                        axios.get('/scenario/executor/getExecutionResult',
                            {
                                params: {
                                    scenarioId: scenarioId
                                }
                            })
                            .then(function (response) {
                                var data = response.data;
                                if (data) {
                                    _this.scenario.LastExecutedStatus = data.IsSuccess ? 1 : 2;
                                    _this.scenario.LastExecutionTime = data.ExecutionTime;
                                    _this.scenario.LastExecutionStackTrace = data.StackTrace;
                                    stopInterval(interval);
                                }
                            })
                            .catch(function(error) {
                                console.log(error);
                                stopInterval(interval);
                            });
                    }, 2000);
                };

                axios.post('/scenario/executor/runSingle',
                    null,
                    {
                        params: {
                            scenarioId: scenarioId
                        }
                    })
                    .then(function () {
                        checkResult();
                    });
            },
            remove: function() {
                this.$emit('remove');
            }
        },
        created: function () {
            var _this = this;
            EventBus.$on("runAll", () => {
                _this.isExecuting = true;

                var stopInterval = function (interval) {
                    clearInterval(interval);
                    _this.isExecuting = false;
                };

                var getExecutingResult = setInterval(function () {
                    var executingResult = _this.$root.getExecutingResult(_this.scenario.Id);

                    if (executingResult) {
                        _this.scenario.LastExecutedStatus = executingResult.IsSuccess ? 1 : 2;
                        _this.scenario.LastExecutionTime = executingResult.ExecutionTime;
                        _this.scenario.LastExecutionStackTrace = executingResult.StackTrace;

                        stopInterval(getExecutingResult);
                    }
                }, 6000);
            });
        }
    });

Vue.component('folder',
    {
        template: "#folder-template",
        props: ['folder'],
        data: function () {
            return {
                isOpen: false,
                isRenaming: false,
                folderNameBeforeRenaming: this.folder.Name
            }
        },
        methods: {
            startRenaming: function () {
                this.isRenaming = true;
                this.folderNameBeforeRenaming = this.folder.Name;
                this.$nextTick(() => this.$refs.renameInput.focus());
            },
            rename: function () {
                var _this = this;

                axios.post('/scenario/list/renameFolder',
                    {
                        Id: _this.folder.Id,
                        Name: _this.folder.Name
                    })
                    .catch(function (error) {
                        _this.folder.Name = _this.folderNameBeforeRenaming;
                        console.log(error);
                    })
                    .then(function () {
                        _this.isRenaming = false;
                    });
            },
            createNewFolder: function () {
                var _this = this;

                axios.post('/scenario/list/createFolder',
                        {
                            ParentId: this.folder.Id,
                            Name: "new folder"
                        })
                    .then(function (response) {
                        var newFolder = response.data;
                        _this.folder.Folders.push(newFolder);
                        _this.isOpen = true;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        }
    });

Vue.component('trash',
    {
        template: "#trash-template",
        props: ['scenarios'],
        data: function () {
            return {
                isOpen: false
            }
        },
        watch: {
            scenarios: function() {
                this.isOpen = true;
            }
        },
        methods: {
            removeScenario: function (scenario) {
                var _this = this;

                axios.post('/scenario/list/deleteScenario',
                        null,
                        {
                            params: {
                                scenarioId: scenario.Id
                            }
                        })
                    .then(function (response) {
                        var index = _this.scenarios.indexOf(scenario);
                        _this.scenarios.splice(index, 1);
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            },
            restoreScenario: function(scenario) {
                var _this = this;

                axios.post('/scenario/list/restoreScenario',
                        null,
                        {
                            params: {
                                scenarioId: scenario.Id
                            }
                        })
                    .then(function (response) {
                        var index = _this.scenarios.indexOf(scenario);
                        _this.scenarios.splice(index, 1);
                        _this.$root.scenarioList.RootScenarios.push(scenario);
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        }
    });