using System.Threading.Tasks;
using Address.Domain;
using Address.Tools;
using AddressDto;
using AddressEFRepository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Utils;
using Xunit;

namespace Address.UnitTest
{
    public class AddressServiceUnitTest
    {
        /// <summary>
        /// ��ӵ�ַ
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Add_Address_ReturnZero()
        {
            DbContextOptions<AddressContext> options = new DbContextOptionsBuilder<AddressContext>().UseInMemoryDatabase("Add_Address_Database").Options;
            var addressContext = new AddressContext(options);

            var createAddressStub = new AddressCreateDto
            {
                City = "����",
                County = "�廪��",
                Province = "����ʡ"
            };
            var addressRepositoryMock = new Mock<IRepository<Domain.Address>>();
            var provinceRepositoryMock = new Mock<IRepository<Province>>();
            var addressUnitOfWork = new AddressUnitOfWork<AddressContext>(addressContext);

            var addressServiceMock = new AddressServiceImpl.AddressServiceImpl(addressRepositoryMock.Object, provinceRepositoryMock.Object, addressUnitOfWork);
            await addressServiceMock.CreateAddressAsync(createAddressStub);
            Assert.Equal(1, await addressContext.Addresses.CountAsync());
        }

        /// <summary>
        /// ������ַ
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Retrieve_Address_ReturnAddressDto()
        {
            var address = new Domain.Address
            {
                City = "������",
                County = "�廪��",
                Province = "����ʡ"
            };

            var addressRepositoryMock = new Mock<IRepository<Domain.Address>>();
            addressRepositoryMock.Setup(q => q.RetrieveAsync(address.ID)).ReturnsAsync(address);

            var provinceRepositoryMock = new Mock<IRepository<Province>>();
            var addressUnitOfWorkMock = new Mock<IAddressUnitOfWork>();

            var addressServiceMock = new AddressServiceImpl.AddressServiceImpl(addressRepositoryMock.Object, provinceRepositoryMock.Object, addressUnitOfWorkMock.Object);
            AddressDto.AddressDto addressDto = await addressServiceMock.RetrieveAsync(address.ID);
            Assert.Equal(address.ID, addressDto.ID);
            Assert.Equal(address.City, addressDto.City);
            Assert.Equal(address.County, addressDto.County);
            Assert.Equal(address.Province, addressDto.Province);
        }
    }
}