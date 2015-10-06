Umbraco CMS

This fork is designed to run on PostgreSQL as an additional Umbraco datasources 

Currently builds, runs and  completes installation for v 7.3. 
Uses Postgres 9.4 and npgsql 2.2.7
I have a running instance of Umbraco at http://postresqltest.corsjune.com/ which is powered by PostgreSQL. 

Some notes:

In this fork, I force all PostgreSQL database objects to lowercase. This is due to problems due to Umbracos camel casing,  inconsistent quoting of schema objects and PostgreSQL's manner of case insensitivity. 

In addition, PetaPoco objects were modified to expect PostgreSQL to have lowercase schemas. In addition most significantly, PetaPoco was modified to use an internal Expando object (sourced from the Massive project) that is case insensitive rather than the default System.Dynamic.ExpandoObject. This was done as the PetaPoco was building case sensitive POCOS (derived from Expando) causing Umbraco to expecting camel case objects and PostgreSQL returning lowercase. If the POCO is case insensitive, problem solved. 

Last of all, I added operators for Boolean-->Int comparisons, PostgreSQL specific Database laters (both old design and new), and provided in the UI the capability to choose PostgreSQL as a data store. 

Todo:

Extensive testing and bug fixes Custom connection strings


===========
Umbraco is a free open source Content Management System built on the ASP.NET platform.

## Building Umbraco from source ##
The easiest way to get started is to run `build/build.bat` which will build both the backoffice (also known as "Belle") and the Umbraco core. You can then easily start debugging from Visual Studio, or if you need to debug Belle you can run `grunt dev` in `src\Umbraco.Web.UI.Client`.
 
If you're interested in making changes to Belle make sure to read the [Belle ReadMe file](src/Umbraco.Web.UI.Client/README.md). Note that you can always [download a nightly build](http://nightly.umbraco.org/umbraco%207.0.0/) so you don't have to build the code yourself.

## Watch a five minute introduction video ##

[![ScreenShot](http://umbraco.com/images/whatisumbraco.png)](http://umbraco.org/help-and-support/video-tutorials/getting-started/what-is-umbraco)

## Umbraco - the simple, flexible and friendly ASP.NET CMS ##

**More than 177,000 sites trust Umbraco** 

For the first time on the Microsoft platform a free user and developer friendly CMS that makes it quick and easy to create websites - or a breeze to build complex web applications. Umbraco has award-winning integration capabilities and supports ASP.NET MVC or Web Forms, including User and Custom Controls, out of the box. It's a developers dream and your users will love it too. 

Used by more than 177,000 active websites including [http://daviscup.com](http://daviscup.com), [http://heinz.com](http://heinz.com), [http://peugeot.com](http://peugeot.com), [http://www.hersheys.com/](http://www.hersheys.com/) and **The Official ASP.NET and IIS.NET website from Microsoft** ([http://asp.net](http://asp.net) / [http://iis.net](http://iis.net)) you can be sure that the technology is proven, stable and scales.  

To view more examples please visit [http://umbraco.com/why-umbraco/#caseStudies](http://umbraco.com/why-umbraco/#caseStudies)

## Downloading ##

The downloadable Umbraco releases live at [http://our.umbraco.org/download](http://our.umbraco.org/download).

## Forums ##

We have a forum running on [http://our.umbraco.org](http://our.umbraco.org). The discussions group on [Google Groups](https://groups.google.com/forum/#!forum/umbraco-dev) is for discussions on developing the core, and not on Umbraco-implementations or extensions in general. For those topics, please use [http://our.umbraco.org](http://our.umbraco.org).

## Contribute to Umbraco ##

If you want to contribute back to Umbraco you should check out our [guide to contributing](http://our.umbraco.org/contribute).

## Found a bug? ##

Another way you can contribute to Umbraco is by providing issue reports, for information on how to submit an issue report refer to our [online guide for reporting issues](http://our.umbraco.org/contribute/report-an-issue-or-request-a-feature).

To view existing issues please visit [http://issues.umbraco.org](http://issues.umbraco.org)
