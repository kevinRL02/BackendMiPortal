namespace UserService1.Dtos{
    public class UserReadDto{
        public int Id{get;set;}
        public required string Name { get; set; }
        public required string Email {get;set;}

        public required string Password {get;set;}

    }
}