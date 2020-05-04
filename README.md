# Automated test library for ASP.NET projects #

### Prototype of idea to write/execute tests of code classes from browser. The solution based on [`System.Reflection`](https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/reflection) to collect the testing data of target assembly.

### Supported members:
* Properties
* Constructors
* Fields
* Methods

## Getting Started

1. Create NuGet package from [`Scenario`](Scenario/Scenario.Core.nuspec) project.
2. Add a reference to the newly created NuGet package to your solution.
3. Configure `Startup.cs` 
<pre>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider diService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            <span style="color: green">app.UseScenario();</span>
            app.UseHttpsRedirection();
            app.UseMvc();
        }
</pre>
4. Setup your testable classes using data annotations as described below.
5. Run solution.

## Data Annotations

[`[ScenarioActivation(ActivationMode)]`](Scenario.Annotations/ScenarioActivationAttribute.cs) enables your class or its members to be included in tests.
Three `ActivationMode` are available:
1. `ActivationMode.All` includes entire class
2. `ActivationMode.Single` includes selected member
3. `ActivationMode.None` exlcludes selected member (can be combined with `ActivationMode.All`)

[`[ScenarioDescription(System.String, System.String)]`](Scenario.Annotations/ScenarioDescriptionAttribute.cs) adds description what class represents of.

## Test creation

![test creation example](test-creation.gif)

The test creation consists of three actions:
1. Assign – create a named variable
2. Execute – create a new object instance or invoke method
3. Assert – compare an actual result and expected one

***

## Test execution

![test execution example](test-execution.gif)

This page is pretty simple and shouldn't cause any problem.