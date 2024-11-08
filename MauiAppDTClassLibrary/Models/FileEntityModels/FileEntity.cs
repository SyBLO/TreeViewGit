
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MauiAppDTClassLibrary.Models.FileEntityModels
{
    public class FileEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FilePath { get; set; } // Chemin complet du fichier, utilisé comme clé unique
        public string FileName { get; set; }
        public bool IsDirectory { get; set; } // Indicateur pour savoir si c'est un répertoire
        public string Extension { get; set; }
        public long Size { get; set; } // Taille en octets (sera convertie en kB dans l'affichage)
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateIndexed { get; set; }

        // Propriété calculée pour afficher la taille en kB
        [NotMapped]
        public string DisplaySize => IsDirectory ? "-" : $"{Size / 1024.0:F2} kB";
    }
}
