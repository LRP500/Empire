using Empire.Attributes;

namespace Empire
{
    public class Perk
    {
        public Attribute Attribute { get; }
        public AttributeModifier Modifier { get;}
        public bool IsActive { get; private set; }

        public Perk(PerkInfo info)
        {
            Attribute = info.Attribute;
            Modifier = (AttributeModifier)System.Activator.CreateInstance(info.ModifierType);
        }

        public void Activate()
        {
            Attribute.AddModifier(Modifier);
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                Attribute.RemoveModifier(Modifier);
            }
        }
    }
}
