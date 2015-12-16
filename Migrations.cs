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
                    .Column<string>("RawElements", c => c.Unlimited())
                );

            // Create a Head content type 
            ContentDefinitionManager.AlterTypeDefinition("Head", cfg => cfg
                .WithPart("HeadPart")
            );

            // Create a new widget content type 
            ContentDefinitionManager.AlterTypeDefinition("HeadWidget", cfg => cfg
                .WithPart("HeadPart")
                .WithPart("WidgetPart")
                .WithSetting("Stereotype", "Widget"));

            ContentDefinitionManager.AlterPartDefinition("HeadPart", builder => builder
               .Attachable()
               .WithDescription("Enables content items to have HTML metadata tags attached."));


            return 3;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition("Head", cfg => cfg
                .RemovePart("CommonPart")
                );
            ContentDefinitionManager.AlterTypeDefinition("HeadWidget", cfg => cfg
                .RemovePart("CommonPart")
                );
            return 2;
        }

        public int UpdateFrom2()
        {
            // Needed because I omitted a migration earlier so I am dropping
            // the table completely rather than trying to repair it.

            SchemaBuilder.DropTable("HeadPartRecord");
            
            try
            {
                SchemaBuilder.DropTable("HeadElementRecord");
            }
            catch
            {
                // ignore table does not exist error
            }

            SchemaBuilder.CreateTable("HeadPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("RawElements", c => c.Unlimited())
                );

            return 3;
        }

    }
}