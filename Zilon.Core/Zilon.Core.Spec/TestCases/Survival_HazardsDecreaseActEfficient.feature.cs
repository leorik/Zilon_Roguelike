﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Zilon.Core.Spec.TestCases
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("Survival_HazardsDecreaseActEfficient", Description="\tЧтобы избегать получение угроз выживания (голод/жажда)\r\n\tКак игроку\r\n\tМне нужно," +
        " чтобы угрозы снижали характеристики эффективность тактических действий.", SourceFile="TestCases\\Survival_HazardsDecreaseActEfficient.feature", SourceLine=0)]
    public partial class Survival_HazardsDecreaseActEfficientFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Survival_HazardsDecreaseActEfficient.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Survival_HazardsDecreaseActEfficient", "\tЧтобы избегать получение угроз выживания (голод/жажда)\r\n\tКак игроку\r\n\tМне нужно," +
                    " чтобы угрозы снижали характеристики эффективность тактических действий.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока_(string mapSize, string personSid, string actorNodeX, string actorNodeY, string startEffect, string equipmentSid, string slotIndex, string tacticalActSid, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "survival",
                    "dev0"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Угрозы выживания (имеются изначально) снижают эффективность тактических действий " +
                    "у актёра игрока.", null, @__tags);
#line 7
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 8
 testRunner.Given(string.Format("Есть карта размером {0}", mapSize), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
 testRunner.And(string.Format("Есть актёр игрока класса {0} в ячейке ({1}, {2})", personSid, actorNodeX, actorNodeY), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And(string.Format("В инвентаре у актёра игрока есть предмет: {0}", equipmentSid), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And(string.Format("Актёр имеет эффект {0}", startEffect), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.When(string.Format("Экипирую предмет {0} в слот Index: {1}", equipmentSid, slotIndex), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
 testRunner.Then(string.Format("Тактическое умение {0} имеет дебафф на эффективность", tacticalActSid), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Угрозы выживания (имеются изначально) снижают эффективность тактических действий " +
            "у актёра игрока., Variant 0", new string[] {
                "survival",
                "dev0"}, SourceLine=16)]
        public virtual void УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока__Variant0()
        {
#line 7
this.УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока_("2", "captain", "0", "0", "Слабый голод", "short-sword", "2", "slash", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Угрозы выживания (имеются изначально) снижают эффективность тактических действий " +
            "у актёра игрока., Variant 1", new string[] {
                "survival",
                "dev0"}, SourceLine=16)]
        public virtual void УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока__Variant1()
        {
#line 7
this.УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока_("2", "captain", "0", "0", "Голод", "short-sword", "2", "slash", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Угрозы выживания (имеются изначально) снижают эффективность тактических действий " +
            "у актёра игрока., Variant 2", new string[] {
                "survival",
                "dev0"}, SourceLine=16)]
        public virtual void УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока__Variant2()
        {
#line 7
this.УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока_("2", "captain", "0", "0", "Голодание", "short-sword", "2", "slash", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Угрозы выживания (имеются изначально) снижают эффективность тактических действий " +
            "у актёра игрока., Variant 3", new string[] {
                "survival",
                "dev0"}, SourceLine=16)]
        public virtual void УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока__Variant3()
        {
#line 7
this.УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока_("2", "captain", "0", "0", "Слабая жажда", "short-sword", "2", "slash", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Угрозы выживания (имеются изначально) снижают эффективность тактических действий " +
            "у актёра игрока., Variant 4", new string[] {
                "survival",
                "dev0"}, SourceLine=16)]
        public virtual void УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока__Variant4()
        {
#line 7
this.УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока_("2", "captain", "0", "0", "Жажда", "short-sword", "2", "slash", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Угрозы выживания (имеются изначально) снижают эффективность тактических действий " +
            "у актёра игрока., Variant 5", new string[] {
                "survival",
                "dev0"}, SourceLine=16)]
        public virtual void УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока__Variant5()
        {
#line 7
this.УгрозыВыживанияИмеютсяИзначальноСнижаютЭффективностьТактическихДействийУАктёраИгрока_("2", "captain", "0", "0", "Обезвоживание", "short-sword", "2", "slash", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
}
#pragma warning restore
#endregion

