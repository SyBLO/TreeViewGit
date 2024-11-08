using MauiAppDTClassLibrary.Models;


namespace MauiAppDT.Services
{
    public class ExampleService
    {
        Example[] allExamples = new[] {
        new Example()
        {
            Name = "To Do List",
            Path = "/todolist",
            Icon = "&#xf1be"
        },

        new Example()
        {
            Name = "Follow Up",
            Icon = "&#xe896",
            Children = new [] {

                new Example()
                {
                    Name = "ECR",
                    Path = "/flowupecr",
                    Title = "Etude",
                    Icon = "&#xe871",
                    Tags = new[] { "color", "background", "border", "utility", "css", "var"}
                },

                new Example()
                {
                    Name = "BE-ECR",
                    Path = "/etude",
                    Title = "Etude",
                    Icon = "&#xe871",
                    Tags = new[] { "color", "background", "border", "utility", "css", "var"}
                },

                new Example()
                {
                    Name = "CMM",
                    Title = "CMM",
                    Path = "/cmmpage",
                    Icon = "&#xe3ec",
                    Tags = new [] { "icon", "content" }
                },

                new Example()
                {
                    New = true,
                    Name = "PROD<->BE",
                    Path = "/notificationpage",
                    Title = "Notification",
                    Icon = "&#xe85a",
                    Tags = new [] { "typo", "typography", "text", "paragraph", "header", "heading", "caption", "overline", "content" }
                },
              
                new Example()
                {
                    New = true,
                    Name = "Project<->BE",
                    Title = "Project",
                    Path = "/ppage",
                    Icon = "&#xf0c5",
                    Tags = new [] { "icon", "content" }
                }
            }
       },

            new Example()
            {
            Name = "Data",
            Icon = "&#xe1db",
            Children = new [] {
                new Example()
                {
                    Name = "ADT",
                    Updated=true,
                    Path = "/adtpage",
                    Title = "Blazor theme colors",
                    Icon = "&#xe23e",
                    Tags = new[] { "color", "background", "border", "utility", "css", "var"}
                },
                new Example()
                {
                    Name = "ECO",
                    Path = "/ecopage",
                    Title = "Blazor Text component",
                    Icon = "&#xe22b",
                   
                },
                 new Example()
                 {
                    Name = "Archivage",
                    Path = "/file-manager",
                    Title = "Blazor Text component",
                    Icon = "&#xe22b",
                    Tags = new [] { "icon", "content" }

                 },
                 new Example()
                 {
                    Name = "Arachivage fichiers",
                    Path = "/filemanager",
                    Title = "Blazor Text component",
                    Icon = "&#xe22b",
                    Tags = new [] { "icon", "content" }

                 },
                    new Example()
                 {
                    Name = "Recherche fichiers",
                    Path = "/test",
                    Title = "Blazor Text component",
                    Icon = "&#xe22b",
                    Tags = new [] { "icon", "content" }

                 }
                 }
             },
            new Example()
            {
            Name = "admin",
            Path = "/admin",
            Icon = "&#xf1be"
            },

    };
        

        public IEnumerable<Example> Examples
        {
            get
            {
                return allExamples;
            }
        }

        public IEnumerable<Example> Filter(string term)
        {
            if (string.IsNullOrEmpty(term))
                return allExamples;

            bool contains(string value) => value != null && value.Contains(term, StringComparison.OrdinalIgnoreCase);

            bool filter(Example example) => contains(example.Name) || (example.Tags != null && example.Tags.Any(contains));

            bool deepFilter(Example example) => filter(example) || example.Children?.Any(filter) == true;

            return Examples.Where(category => category.Children?.Any(deepFilter) == true || filter(category))
                           .Select(category => new Example
                           {
                               Name = category.Name,
                               Path = category.Path,
                               Icon = category.Icon,
                               Expanded = true,
                               Children = category.Children?.Where(deepFilter).Select(example => new Example
                               {
                                   Name = example.Name,
                                   Path = example.Path,
                                   Icon = example.Icon,
                                   Expanded = true,
                                   Children = example.Children
                               }
                               ).ToArray()
                           }).ToList();
        }    
       
    }
}
