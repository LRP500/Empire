using Empire.Attributes;
using UnityEngine;
using Attribute = Empire.Attributes.Attribute;

namespace Empire.Testing
{
    public class AttributeTest : MonoBehaviour
    {
        [SerializeField]
        private Attribute _attribute;

        private void Awake()
        {
            _attribute.Log();
            _attribute.AddModifier(new BaseAddAttributeModifier(25));
            _attribute.Log();
            _attribute.AddModifier(new BasePercentAttributeModifier(10));
            _attribute.Log();
            _attribute.AddModifier(new TotalPercentAttributeModifier(10));
            _attribute.Log();
        }
    }
}
