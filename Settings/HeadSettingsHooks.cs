using Cascade.Head.Helpers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Cascade.Head.Settings
{
    public class HeadSettingsHooks : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "HeadPart") 
                yield break;

            var model = new HeadTypePartSettings();

            // This is the way you are supposed to do it according to piedonne:
            //var model = definition.Settings.GetModel<HeadTypePartSettings>();

            // but this is what I found works:
            try
            {
                model.RawElements = definition.Settings["HeadTypePartSettings.RawElements"];
                model.Elements = HeadElementSerializer.Deserialize(model.RawElements);
            }
            catch { }

            yield return DefinitionTemplate(model);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(Orchard.ContentManagement.MetaData.Builders.ContentTypePartDefinitionBuilder builder, Orchard.ContentManagement.IUpdateModel updateModel)
        {
            if (builder.Name != "HeadPart")
                yield break;

            var model = new HeadTypePartSettings ();
            updateModel.TryUpdateModel(model, "HeadTypePartSettings", null, new string[] { "RawElements" });

            model.Elements = model.Elements.Where(e => !e.Deleted).ToList();
            model.RawElements = HeadElementSerializer.Serialize(model.Elements);

            builder.WithSetting("HeadTypePartSettings.RawElements", model.RawElements);

            yield return DefinitionTemplate(model);

        }

    }
}