# Cascade.Head
An Orchard CMS Module for maintaining html head tags (link, meta, charset, etc)

After installation you need to add the HeadElementPart to whatever Orchard Content Types are appropriate. 
For example, you will likely want to add the HeadElementPart to the Page Content Type, and maybe also the Blog Post Type.
Although you can add the HeadElementPart to any type, Page and Blog are the prime candidates.

To add head elements to ALL items of the selected content type, use Content Definition to edit the Type Settings. 

To add head elements to a particular item, use Content editing to set the appropriate values for that content item.

Cascade.Head will not work with standard Orchard Themes unless you modify them. Specifically, you need to remove most of the head tags from Document.cshtml
and Resources.cshtml. If you use Cascade themes this has already been done.
