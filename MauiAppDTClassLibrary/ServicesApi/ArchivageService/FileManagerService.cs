

namespace MauiAppDTClassLibrary.ServicesApi.ArchivageService
{
   public class FileNode
{
    public string Name { get; set; }
    public string Path { get; set; }
    public bool IsDirectory { get; set; }
     public List<FileNode> Children { get; set; } = new List<FileNode>();

    }

public class FileManagerService
    {
        public async Task<List<FileNode>> LoadDirectoryAsync(string path, int page = 1, int pageSize = 50)
        {
            var nodes = new List<FileNode>();

            // Charger les répertoires
            var directories = Directory.GetDirectories(path)
                                       .Skip((page - 1) * pageSize)
                                       .Take(pageSize)
                                       .Select(dir => new FileNode
                                       {
                                           Name = Path.GetFileName(dir),
                                           Path = dir,
                                           IsDirectory = true
                                       });
            foreach (var dir in directories)
            {
                nodes.Add(dir);
            }

            // Charger les fichiers
            var files = Directory.GetFiles(path)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Select(file => new FileNode
                                 {
                                     Name = Path.GetFileName(file),
                                     Path = file,
                                     IsDirectory = false
                                 });
            foreach (var file in files)
            {
                nodes.Add(file);
            }

            return await Task.FromResult(nodes);
        }

        public async Task<List<FileNode>> LoadSubdirectoriesAsync(string path, int page = 1, int pageSize = 50)
        {
            var nodes = new List<FileNode>(
                        Directory.GetDirectories(path)
                                 .Skip((page - 1) * pageSize)
                                 .Take(pageSize)
                                 .Select(dir => new FileNode
                                 {
                                     Name = Path.GetFileName(dir),
                                     Path = dir,
                                     IsDirectory = true
                                 })
                                 ); 
            return await Task.FromResult(nodes);
        }

        public async Task<bool> MoveFileOrDirectoryAsync(string sourcePath, string targetPath)
        {
            try
            {
                // Si targetPath est un dossier, combinez avec le nom du fichier source
                string destinationPath = targetPath;
                if (Directory.Exists(targetPath))
                {
                    // targetPath est un dossier, donc on ajoute le nom du fichier source
                    var fileName = Path.GetFileName(sourcePath);
                    destinationPath = Path.Combine(targetPath, fileName);
                }

                // Vérifie si un fichier ou un dossier existe déjà au chemin de destination
                if (File.Exists(destinationPath) || Directory.Exists(destinationPath))
                {
                    Console.WriteLine("Le fichier ou répertoire de destination existe déjà : " + destinationPath);
                    return false;
                }

                // Déplace le fichier ou le dossier
                if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, destinationPath);
                }
                else if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destinationPath);
                }
                else
                {
                    Console.WriteLine("Le fichier ou répertoire source n'existe pas : " + sourcePath);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du déplacement : " + ex.Message);
                return false;
            }
        }
        public async Task<List<FileNode>> SearchTreeNodesAsync(List<FileNode> nodes, string searchTerm, List<string> debugMessages)
        {
            debugMessages.Clear();
            debugMessages.Add($"Début de la recherche pour le terme : {searchTerm}");

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                debugMessages.Add("Le terme de recherche est vide, retour de tous les nœuds.");
                return nodes;
            }

            var result = new List<FileNode>();

            foreach (var node in nodes)
            {
                debugMessages.Add($"Recherche dans le nœud : {node.Name}");

                bool isMatch = node.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);

                if (isMatch)
                {
                    debugMessages.Add($"Correspondance trouvée : {node.Name}");

                    result.Add(new FileNode
                    {
                        Name = node.Name,
                        Path = node.Path,
                        IsDirectory = node.IsDirectory,
                        Children = new List<FileNode>()
                    });
                }
                else if (node.IsDirectory)
                {
                    debugMessages.Add($"Le nœud {node.Name} est un dossier, chargement des enfants...");

                    if (node.Children == null || node.Children.Count == 0)
                    {
                        node.Children = await LoadSubdirectoriesAsync(node.Path);
                        node.Children.AddRange(await LoadDirectoryAsync(node.Path));
                        debugMessages.Add($"Enfants chargés pour le dossier : {node.Name}, Nombre d'enfants : {node.Children.Count}");
                    }

                    var filteredChildren = await SearchTreeNodesAsync(node.Children, searchTerm, debugMessages);
                    if (filteredChildren.Count > 0)
                    {
                        debugMessages.Add($"Des enfants correspondent au terme de recherche dans le dossier : {node.Name}");

                        result.Add(new FileNode
                        {
                            Name = node.Name,
                            Path = node.Path,
                            IsDirectory = true,
                            Children = filteredChildren
                        });
                    }
                    else
                    {
                        debugMessages.Add($"Aucun enfant correspondant trouvé dans le dossier : {node.Name}");
                    }
                }
            }

            debugMessages.Add($"Recherche terminée, nombre de résultats trouvés : {result.Count}");
            return result;
        }


    }
}
