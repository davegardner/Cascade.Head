# Cascade.Head Ver 1.1
An Orchard CMS Module for maintaining html head tags (title, link, meta, charset, etc)

Head Elements can be added within three different scopes:

1. Site Settings allow Head Elements to be added globally. These Head Elements will apear on every public page (not on admin pages).

1. Content Type Settings allows Head Elements to be spcifified for a particular content type, such as a Page or a Blog. 
These elements are displayed for all content that type, 
For example, if an element is specified for a Blog Type then it will be displayed on every Blog page, but not on ordinary pages.

1. Content settings allow a Head Element to be attached to a particular piece of content, and displayed whenever that content is included in a page.

For Head Elements to be attached to Content Type and Content, you must first add the Head Part to the particular Content Type.

For example, you will likely want to add the HeadElementPart to the Page Content Type, and maybe also the Blog Post Type.
Although you can add the HeadElementPart to any type, Page and Blog are the prime candidates.

#Warning
Cascade.Head will not work with standard Orchard Themes unless you modify them. Specifically, you need to remove most of the head tags from Document.cshtml
and Resources.cshtml. If you use Cascade themes this has already been done.

#Copyright
&copy; Copyright 2015 Dave Gardner, Cascade Pixels. All Rights Reserved.
