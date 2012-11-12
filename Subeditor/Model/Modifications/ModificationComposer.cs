using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.Modifications
{
    class ModificationComposer
    {
        private IList<IModification> modifications;

        public ModificationComposer()
        {
            modifications = new List<IModification>();
        }

        public bool IsComposing { get; private set; }

        public void Begin()
        {
            IsComposing = true;

            modifications.Clear();
        }

        public CompositeModification End()
        {
            IsComposing = false;

            CreateAndAssignGroup();

            return new CompositeModification(modifications); ;
        }

        public void Add(IModification modification)
        {
            modifications.Add(modification);
        }

        private void CreateAndAssignGroup()
        {
            ModificationGroup currentGroup = new ModificationGroup(modifications);
            for (int i = 0; i < modifications.Count; ++i)
            {
                if (modifications[i] is IGroupableModification)
                {
                    var currentMod = modifications[i] as IGroupableModification;

                    currentMod.Group = currentGroup;
                    currentMod.GroupIndex = i;
                }
            }
        }

    }
}
