using System.Data;

namespace AnettaNail.Models
{
    public class Kasutajad
    {
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Parool { get; set; }
        public string Email { get; set; }
        public string TelefoniNumber { get; set; }
        public int RollId { get; set; } // Внешний ключ для связи с ролями пользователей
        public Roll Roll { get; set; } // Навигационное свойство для доступа к роли пользователя
    }
}
