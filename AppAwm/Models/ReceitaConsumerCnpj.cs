namespace AppAwm.Models
{
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Address
        {
            public int municipality { get; set; }
            public string? Street { get; set; }
            public string? Number { get; set; }
            public string? Details { get; set; }
            public string? District { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? Zip { get; set; }
            public required Country Country { get; set; }
        }

        public class Agent
        {
            public Role Role { get; set; }
            public Person Person { get; set; }
        }

        public class Company
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int Equity { get; set; }
            public Nature? Nature { get; set; }
            public Size? Size { get; set; }
            public List<Member>? Members { get; set; }
        }

        public class Country
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
        }

        public class Email
        {
            public string? Address { get; set; }
            public string? Domain { get; set; }
        }

        public class MainActivity
        {
            public int? Id { get; set; }
            public string? Text { get; set; }
        }

        public class Member
        {
            public string? Since { get; set; }
            public Role? Role { get; set; }
            public Person? Person { get; set; }
            public Agent? Agent { get; set; }
        }

        public class Nature
        {
            public int Id { get; set; }
            public string? Text { get; set; }
        }

        public class Person
        {
            public string? Id { get; set; }
            public string? Name { get; set; }
            public string? Type { get; set; }
            public string? TaxId { get; set; }
            public string? Age { get; set; }
        }

        public class Phone
        {
            public string? Area { get; set; }
            public string? Number { get; set; }
        }

        public class Role
        {
            public int Id { get; set; }
            public string? Text { get; set; }
        }

        public class ReceitaConsumerCnpj
        {
            public DateTime? Updated { get; set; }
            public string? YaxId { get; set; }
            public Company? Company { get; set; }
            public object? Alias { get; set; }
            public string? Founded { get; set; }
            public bool? Head { get; set; }
            public string? StatusDate { get; set; }
            public Status? Status { get; set; }
            public Address? Address { get; set; }
            public List<Phone>? Phones { get; set; }
            public List<Email>? Emails { get; set; }
            public MainActivity? MainActivity { get; set; }
            public List<SideActivity>? SideActivities { get; set; }
        }

        public class SideActivity
        {
            public int? Id { get; set; }
            public string? Text { get; set; }
        }

        public class Size
        {
            public int? Id { get; set; }
            public string? Acronym { get; set; }
            public string? Text { get; set; }
        }

        public class Status
        {
            public int Id { get; set; }
            public string? Text { get; set; }
        }
}
