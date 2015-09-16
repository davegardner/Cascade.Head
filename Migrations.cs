using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Cascade.Head
{

    public class HtmlMetaDataMigrations : DataMigrationImpl
    {
        public int Create()
        {
            SchemaBuilder.CreateTable("HeadElementRecord",
                table => table
                    .Column<int>("Id", c=>c.PrimaryKey().Identity())
                    .Column<int>("HeadPartRecord_Id")
                    .Column<string>("Type")
                    .Column<string>("Name")
                    .Column<string>("Content", c => c.Unlimited())
                );

            // Groups a collection of elements together
            SchemaBuilder.CreateTable("HeadPartRecord",
                table => table
                    .ContentPartRecord()
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
               .WithDescription("Allows content items to have HTML metadata tags attached."));


            return 1;
        }

        //public int UpdateFrom1()
        //{
        //    ContentDefinitionManager.AlterTypeDefinition("HeadElement",
        //       cfg => cfg
        //           .WithPart("HeadElementPart")
        //           .WithPart("CommonPart", p => p.WithSetting("OwnerEditorSettings.ShowOwnerEditor", "false"))
        //        );
        //    return 2;
        //}
    }
}