# Cascade.Head
An Orchard CMS Module for maintaining html head tags (link, meta, charset, etc)

After installation you need to add the HeadElementPart to whatever Orchard Content Types are appropriate. 
For example, you will likely want to add the HeadElementPart to the Page Content Type, and maybe also the Blog Post Type.
Although you can add the HeadElementPart to any type, it's unlikeley that you would want to add it to anything other than a Page or a Blog.

Once the Content Type has been configured, you can enter head elements whenever you add or edit content types. 

Cascade.Head will not work with standard Orchard Themes unless you modify them. Specifically, you need to remove most of the head tags from Document.cshtml
and Resources.cshtml. If you use Cascade themes this has already been done.
