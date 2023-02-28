namespace PhoneBook.Server.Repositories
{
    public class PhoneBookRepository
    {
        public List<ContactModel> Contacts { get; set; }
        private readonly Random _random = new();

        public PhoneBookRepository()
        {
            InitContacts(10);
        }

        #region Random Seeds
        /// <summary>
        /// Yapay contact nesnesi olusturmak icin kullanilacak olan isim listesi.
        /// </summary>
        private List<string> FirstNamesSeed { get; } = new List<string>
            {
                "Kate", "Lara", "Lena", "Saskia", "Yasmin","Kathleen", "Mya", "Alexandra", "Connie", "Anthony", "Roman", "Adrian", "Zach", "Vincent", "Francis", "Bryan", "Sam", "Keaton", "Isaiah", "Victor"
            };

        /// <summary>
        /// Yapay contact nesnesi olusturmak icin kullanilacak olan soyisim listesi.
        /// </summary>
        private List<string> LastNamesSeed { get; } = new List<string>
            {
                "Roberts", "Hall", "Le", "Soto", "Murphy", "Pineda", "Haley", "Howe", "Molina", "Gilbert", "Johns", "Terry", "Lester", "Contreras", "Finley", "Douglas", "Reid", "Thornton", "McCann", "Valdez"
            };
        #endregion

        #region Helper Methods
        /// <summary>
        /// Ctor icerisinde cagrilir. Uygulama ayaga kalktiginda elimizde hazir contact listesi olmasını saglar.
        /// </summary>
        /// <param name="numberOfContacts">Kac adet contact nesnesi olusturulacagini belirler.</param>
        private void InitContacts(int numberOfContacts)
        {
            Contacts = new List<ContactModel>();
            int contactID = 1;
            for (int i = 0; i < numberOfContacts; i++)
            {
                ContactModel contact = RandomContact();
                contact.Id = contactID;
                Contacts.Add(contact);
                contactID++;
            }
        }

        /// <summary>
        /// Seed veriler içerisinden rastgele bir sekilde contact nesnesi olusturur.
        /// </summary>
        private ContactModel RandomContact()
        {
            return new ContactModel()
            {
                FirstName = this.FirstNamesSeed[_random.Next(0, 20)],
                LastName = this.LastNamesSeed[_random.Next(0, 20)]
            };
        }
        #endregion

        /// <summary>
        /// Contact listesi icerisindeki verileri geri dondurur.
        /// </summary>
        public ContactList GetAll()
        {
            var contactList = new ContactList();
            foreach (var contact in Contacts)
            {
                contactList.Contacts.Add(contact);
            }
            return contactList;
        }

        /// <summary>
        /// Contact'leri olustururken id'leri duzenli atayabilmek icin, en buyuk id'den buyuk olan en kucuk sayiyi dondurur.
        /// </summary>
        public int GetNextContactId()
        {
            return Contacts.Max(x => x.Id) + 1;
        }

        /// <summary>
        /// Gonderilen contact verisini listeye ekler.
        /// </summary>
        /// <param name="contactModel">Client tarafindan gonderilen contact bilgisi..</param>
        public ContactModel AddContact(ContactModel contactModel)
        {
            contactModel.Id = GetNextContactId();
            Contacts.Add(contactModel);
            return contactModel;
        }
    }
}
