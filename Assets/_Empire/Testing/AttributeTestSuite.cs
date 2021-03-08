using Empire.Stats;
using NUnit.Framework;
using UnityEditor;

// Missing Tests :
// - Modifier removing
// - Base stat clamp
// - Update modifiers method
internal class AttributeTestSuite
{
    private IAttribute _attribute;

    #region Setup

    private const string TestResourcePath = "Assets/_Empire/Testing/";
    private const string TestAttributeName = "TestAttribute.asset";

    private static Attribute LoadTestAttribute()
    {
        return AssetDatabase.LoadAssetAtPath<Attribute>(TestResourcePath + TestAttributeName);
    }

    [SetUp]
    public void Setup()
    {
        _attribute = LoadTestAttribute();
        _attribute.SetBaseValue(0);
        _attribute.ClearModifiers();
    }

    #endregion Setup

    [Test]
    [Category("Setup")]
    public void TestSetup()
    {
        Assert.IsNotNull(_attribute);
    }

    [Test]
    public void TestModifiedValueWhenInitialized()
    {
        Assert.AreEqual(_attribute.ModifiedValue, _attribute.BaseValue);
    }

    [Test]
    public void TestBaseValueWhenInitializedWithParameter()
    {
        _attribute.SetBaseValue(25);
        Assert.AreEqual(_attribute.BaseValue, 25);
    }

    [Test]
    public void TestModifiedValueWhenInitializedWithParameter()
    {
        _attribute.SetBaseValue(25);
        Assert.AreEqual(_attribute.ModifiedValue, 25);
    }

    [Test]
    [TestCase(0, 50, ExpectedResult = 50, TestName = "TestPositiveValue")]
    [TestCase(0, -50, ExpectedResult = -50, TestName = "TestNegativeValue")]
    public float TestBaseAddModifier(float baseValue, float modifierValue)
    {
        _attribute.SetBaseValue(baseValue);
        _attribute.AddModifier(new BaseAddAttributeModifier(modifierValue));
        return _attribute.ModifiedValue;
    }

    [Test]
    [TestCase(50, 50f, ExpectedResult = 75, TestName = "TestPositiveValue")]
    [TestCase(50, -50f, ExpectedResult = 25, TestName = "TestNegativeValue")]
    public float TestBasePercentAddModifier(float baseValue, float modifierValue)
    {
        _attribute.SetBaseValue(baseValue);
        _attribute.AddModifier(new BasePercentAttributeModifier(modifierValue));
        return _attribute.ModifiedValue;
    }

    [Test]
    public void TestBaseAddBeforeBasePercentModifier()
    {
        _attribute.SetBaseValue(50);
        _attribute.AddModifier(new BaseAddAttributeModifier(100));
        _attribute.AddModifier(new BasePercentAttributeModifier(100));
        Assert.AreEqual(_attribute.ModifiedValue, 200);
    }

    [Test]
    public void TestBasePercentBeforeBaseAddModifier()
    {
        _attribute.SetBaseValue(50);
        _attribute.AddModifier(new BasePercentAttributeModifier(100));
        _attribute.AddModifier(new BaseAddAttributeModifier(100));
        Assert.AreEqual(_attribute.ModifiedValue, 200);
    }

    [Test]
    [TestCase(50, 100, ExpectedResult = 150, TestName = "TestPositiveValue")]
    [TestCase(50, -100, ExpectedResult = -50, TestName = "TestNegativeValue")]
    public float TestTotalAddModifier(float baseValue, float modifierValue)
    {
        _attribute.SetBaseValue(baseValue);
        _attribute.AddModifier(new TotalAddAttributeModifier(modifierValue));
        return _attribute.ModifiedValue;
    }

    [Test]
    [TestCase(50, 100f, ExpectedResult = 100, TestName = "TestPositiveValue")]
    [TestCase(50, -100f, ExpectedResult = 0, TestName = "TestNegativeValue")]
    public float TotalPercentAddModifier(float baseValue, float modifierValue)
    {
        _attribute.SetBaseValue(baseValue);
        _attribute.AddModifier(new TotalPercentAttributeModifier(modifierValue));
        return _attribute.ModifiedValue;
    }

    [Test]
    public void TestTotalAddBeforeTotalPercentModifier()
    {
        _attribute.SetBaseValue(50);
        _attribute.AddModifier(new TotalAddAttributeModifier(100));
        _attribute.AddModifier(new TotalPercentAttributeModifier(100));
        Assert.AreEqual(_attribute.ModifiedValue, 200);
    }

    [Test]
    public void TestTotalPercentBeforeTotalAddModifier()
    {
        _attribute.SetBaseValue(50);
        _attribute.AddModifier(new TotalPercentAttributeModifier(100));
        _attribute.AddModifier(new TotalAddAttributeModifier(100));
        Assert.AreEqual(_attribute.ModifiedValue, 200);
    }

    [Test]
    public void TestBasePercentBeforeTotalPercentModifier()
    {
        _attribute.SetBaseValue(50);
        _attribute.AddModifier(new BasePercentAttributeModifier(100));
        _attribute.AddModifier(new BaseAddAttributeModifier(100));
        _attribute.AddModifier(new TotalPercentAttributeModifier(100));
        Assert.AreEqual(_attribute.ModifiedValue, 400);
    }

    [Test]
    public void TestTotalPercentBeforeBasePercentModifier()
    {
        _attribute.SetBaseValue(50);
        _attribute.AddModifier(new TotalPercentAttributeModifier(100));
        _attribute.AddModifier(new BaseAddAttributeModifier(100));
        _attribute.AddModifier(new BasePercentAttributeModifier(100));
        Assert.AreEqual(_attribute.ModifiedValue, 400);
    }

    [Test]
    public void TestAllModifiersTogether()
    {
        _attribute.SetBaseValue(50);
        _attribute.AddModifier(new BasePercentAttributeModifier(100));
        _attribute.AddModifier(new BaseAddAttributeModifier(100));
        _attribute.AddModifier(new TotalPercentAttributeModifier(100));
        _attribute.AddModifier(new TotalAddAttributeModifier(100));
        Assert.AreEqual(_attribute.ModifiedValue, 500);
    }
}
