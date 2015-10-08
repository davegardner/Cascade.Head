using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Cascade.Head
{

    public class HtmlMetaDataMigrations : DataMigrationImpl
    {
        public int Create()
        {
            // Groups a collection of elements together
            SchemaBuilder.CreateTable("HeadPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("RawElements")
                );

            // Create a Head content type 
            ContentDefinitionManager.AlterTypeDefinition("Head", cfg => cfg
                .WithPart("HeadPart")
                .WithPart("CommonPart")
            );

            // Create a new widget content type 
            ContentDefinitionManager.AlterTypeDefinition("HeadWidget", cfg => cfg
                .WithPart("HeadPart")
                .WithPart("WidgetPart")
                .WithPart("CommonPart")
                .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterPartDefinition("HeadPart", builder => builder
               .Attachable()
               .WithDescription("Enables content items to have HTML metadata tags attached."));


            return 1;
        }

        //public int UpdateFrom1()
        //{
        //    return 2;
        //}
    }
}