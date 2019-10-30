using System.Collections.Generic;
using AddressEFRepository;

namespace Address.IntegrationTest
{
    public static class Utilities
    {
        public static void InitializeDbForTests(AddressContext db)
        {
            List<Domain.Address> addresses = GetSeedingAddresses();
            db.Addresses.AddRange(addresses);
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AddressContext db)
        {
            db.Addresses.RemoveRange(db.Addresses);
            InitializeDbForTests(db);
        }

        public static List<Domain.Address> GetSeedingAddresses()
        {
            return new List<Domain.Address>
            {
                new Domain.Address
                {
                    City = "贵阳",
                    County = "测试县",
                    Province = "贵州省"
                },
                new Domain.Address
                {
                    City = "昆明市",
                    County = "武定县",
                    Province = "云南省"
                },
                new Domain.Address
                {
                    City = "昆明市",
                    County = "五华区",
                    Province = "云南省"
                }
            };
        }
    }
}