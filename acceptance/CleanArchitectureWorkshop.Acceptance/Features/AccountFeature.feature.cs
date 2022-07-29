﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace CleanArchitectureWorkshop.Acceptance.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class AccountFeatureFeature : object, Xunit.IClassFixture<AccountFeatureFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "AccountFeature.feature"
#line hidden
        
        public AccountFeatureFeature(AccountFeatureFeature.FixtureData fixtureData, CleanArchitectureWorkshop_Acceptance_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "AccountFeature", "User can deposit into Account\r\nUser can withdraw from an Account\r\nUser can retrie" +
                    "ve a bank statement to the console", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Prints all statements with balance from newest to oldest")]
        [Xunit.TraitAttribute("FeatureTitle", "AccountFeature")]
        [Xunit.TraitAttribute("Description", "Prints all statements with balance from newest to oldest")]
        [Xunit.TraitAttribute("Category", "Acceptance")]
        public void PrintsAllStatementsWithBalanceFromNewestToOldest()
        {
            string[] tagsOfScenario = new string[] {
                    "Acceptance"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Prints all statements with balance from newest to oldest", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 7
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
        testRunner.Given("I make a deposit of 1000 on \'10 January 2021\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
        testRunner.And("I make a deposit of 2000 on \'15 January 2021\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 10
        testRunner.And("I make a withdrawal of 500 on \'20 January 2021\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 11
        testRunner.When("I retrieve the account statements", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "Date",
                            "Amount",
                            "Balance"});
                table1.AddRow(new string[] {
                            "20 January 2021",
                            "-500",
                            "2500"});
                table1.AddRow(new string[] {
                            "15 January 2021",
                            "2000",
                            "3000"});
                table1.AddRow(new string[] {
                            "10 January 2021",
                            "1000",
                            "1000"});
#line 12
        testRunner.Then("I should see these statements:", ((string)(null)), table1, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Prints all statements while no operations where made on my account")]
        [Xunit.TraitAttribute("FeatureTitle", "AccountFeature")]
        [Xunit.TraitAttribute("Description", "Prints all statements while no operations where made on my account")]
        [Xunit.TraitAttribute("Category", "Acceptance")]
        public void PrintsAllStatementsWhileNoOperationsWhereMadeOnMyAccount()
        {
            string[] tagsOfScenario = new string[] {
                    "Acceptance"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Prints all statements while no operations where made on my account", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 19
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 20
        testRunner.When("I retrieve the account statements", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 21
        testRunner.Then("I should see no statements", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                AccountFeatureFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                AccountFeatureFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion