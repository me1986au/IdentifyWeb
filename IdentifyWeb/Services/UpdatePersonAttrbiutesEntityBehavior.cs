using IdentifyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentifyWeb.Services
{
    public class UpdatePersonEntity
    {
        private string _applicationUserId;

        private PersonDto _personDto;
        private Person _personEntity;
        private ApplicationDbContext _dbContext;

        public UpdatePersonEntity(ApplicationDbContext dbContext, PersonDto personDto, Person personEntity, string applicationUserId)
        {
            _personDto = personDto;
            _personEntity = personEntity;
            _applicationUserId = applicationUserId;
            _dbContext = dbContext;
        }


        public Person PerformAction()
        {

            if (_personDto.IsNew)
            {
                Add();
            }
            else
            {
                var personEntity = _dbContext.Persons.Where(x => x.Id == _personDto.Id && x.ApplicationUserId == _applicationUserId).FirstOrDefault();

                if (personEntity == null)
                {
                    throw new Exception("User doesnt have authority to edit this person");
                }
                _personEntity = personEntity;


                if (_personDto.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Update)
                {
                    Update();
                }

                if (_personDto.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Delete)
                {
                    Delete();
                }

            }

            return _personEntity;

        }


        private void Add()
        {
            _personEntity = new Person();
            TransferInfo();
            _dbContext.Persons.Add(_personEntity);
        }

        private void Update()
        {
            TransferInfo();
            _dbContext.Entry(_personEntity).State = System.Data.Entity.EntityState.Modified;
        }


        private void Delete()
        {
            _dbContext.Entry(_personEntity).State = System.Data.Entity.EntityState.Deleted;

        }


        private void TransferInfo()
        {
            if (_personDto.IsNew)
            {
                    _personEntity.Id = Guid.NewGuid().ToString();
                _personEntity.ApplicationUserId = _applicationUserId;
            }

            _personEntity.FirstName = _personDto.FirstName;
            _personEntity.LastName = _personDto.LastName;
            _personEntity.Alias = _personDto.Alias;
            _personEntity.Gender = _personDto.Gender;
            _personEntity.DateOfBirth = _personDto.DateOfBirth;

        }

    }

    public class UpdatePersonAttributes
    {

        private string _applicationUserId;

        private List<PersonsAttributeDto> _personAttributeDtoList;
        private Person _personEntity;
        private ApplicationDbContext _dbContext;

        public UpdatePersonAttributes(ApplicationDbContext dbContext, List<PersonsAttributeDto> personAttributeDtoList, Person person, string applicationUserId)
        {
            _personAttributeDtoList = personAttributeDtoList;
            _personEntity = person;
            _applicationUserId = applicationUserId;
            _dbContext = dbContext;
        }


        public void PerformAction()
        {
            _personAttributeDtoList.Where(x => x.IsNew).ToList().ForEach(x => Add(x));
            _personAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Update).ToList().ForEach(x => Update(x));
            _personAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Delete).ToList().ForEach(x => Delete(x));

        }



        private void Add(PersonsAttributeDto personsAttributeDto)
        {
            var personsAttributeEntity = new PersonsAttribute();
            TranferInfo(personsAttributeDto, personsAttributeEntity);

            _personEntity.PersonsAttribute.Add(personsAttributeEntity);

            //update subAttrbutes (
            var personalSubAttribute = new UpdatePersonalSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.PersonalSubAttributeDtos.ToList() , _applicationUserId);
            personalSubAttribute.PerformAction();

            var addressSubAttribute = new UpdateAddressSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.AddressSubAttributeDtos.ToList(), _applicationUserId);
            addressSubAttribute.PerformAction();

            var phoneNumberSubAttribute = new UpdatePhoneNumberSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.PhoneNumberSubAttributeDtos.ToList(), _applicationUserId);
            phoneNumberSubAttribute.PerformAction();

            var updateTimeFrameSubAttribute = new UpdateTimeFrameSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.TimeFrameSubAttributeDtos.ToList(), _applicationUserId);
            updateTimeFrameSubAttribute.PerformAction();

        }

        private void Update(PersonsAttributeDto personsAttributeDto)
        {

            var personsAttributeEntity = _personEntity.PersonsAttribute.Where(x => x.Id == personsAttributeDto.Id).FirstOrDefault();

            if (personsAttributeEntity == null)
            {
                throw new Exception("User is not authorised to update this persons attribute");
            }


            TranferInfo(personsAttributeDto, personsAttributeEntity);

            _dbContext.Entry(personsAttributeEntity).State = System.Data.Entity.EntityState.Modified;


            //update subAttrbutes (
            var personalSubAttribute = new UpdatePersonalSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.PersonalSubAttributeDtos.ToList(), _applicationUserId);
            personalSubAttribute.PerformAction();

            var addressSubAttribute = new UpdateAddressSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.AddressSubAttributeDtos.ToList(), _applicationUserId);
            addressSubAttribute.PerformAction();

            var phoneNumberSubAttribute = new UpdatePhoneNumberSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.PhoneNumberSubAttributeDtos.ToList(), _applicationUserId);
            phoneNumberSubAttribute.PerformAction();

            var updateTimeFrameSubAttribute = new UpdateTimeFrameSubAttribute(_dbContext, personsAttributeEntity, personsAttributeDto.TimeFrameSubAttributeDtos.ToList(), _applicationUserId);
            updateTimeFrameSubAttribute.PerformAction();
        }

        private void Delete(PersonsAttributeDto personsAttributeDto)
        {

            var personAttributeEntity = _personEntity.PersonsAttribute.Where(x => x.Id == personsAttributeDto.Id).FirstOrDefault();

            if (personAttributeEntity == null)
            {
                throw new Exception("User is not authorised to delete this persons attribute");
            }

            _dbContext.Entry(personAttributeEntity).State = System.Data.Entity.EntityState.Deleted;
        }



        private void TranferInfo(PersonsAttributeDto dto, PersonsAttribute entity)
        {
            if (dto.IsNew)
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            entity.PersonsAttributeCategoryId = dto.PersonsAttributeCategoryId;

        }


    }

    public class UpdatePersonalSubAttribute
    {

        private string _applicationUserId;

        private List<PersonalSubAttributeDto> _personSubAttributeDtoList;
        private PersonsAttribute _personsAttributeEntity;
        private ApplicationDbContext _dbContext;

        public UpdatePersonalSubAttribute(ApplicationDbContext dbContext, PersonsAttribute personsAttributeEntity, List<PersonalSubAttributeDto> personalSubAttributeDtoList, string applicationUserId)
        {
            _personSubAttributeDtoList = personalSubAttributeDtoList;
            _personsAttributeEntity = personsAttributeEntity;
            _applicationUserId = applicationUserId;
            _dbContext = dbContext;
        }


        public void PerformAction()
        {
            _personSubAttributeDtoList.Where(x => x.IsNew).ToList().ForEach(x => Add(x));
            _personSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Update).ToList().ForEach(x => Update(x));
            _personSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Delete).ToList().ForEach(x => Delete(x));

        }

        private void Add(PersonalSubAttributeDto personalSubAttributeDto)
        {
            var personalSubAttributeEntity = new PersonalSubAttribute();
            TransferInfo(personalSubAttributeDto, personalSubAttributeEntity);

            _personsAttributeEntity.PersonalSubAttribute.Add(personalSubAttributeEntity);

            //update subAttrbutes (

        }

        private void Update(PersonalSubAttributeDto personalSubAttributeDto)
        {

            var personalSubAttributeEntity = _personsAttributeEntity.PersonalSubAttribute.Where(x => x.Id == personalSubAttributeDto.Id).FirstOrDefault();

            if (personalSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to update this personal sub attribute");
            }

            TransferInfo(personalSubAttributeDto, personalSubAttributeEntity);

            _dbContext.Entry(personalSubAttributeEntity).State = System.Data.Entity.EntityState.Modified;

        }

        private void Delete(PersonalSubAttributeDto personalSubAttributeDto)
        {

            var personalSubAttributeEntity = _personsAttributeEntity.PersonalSubAttribute.Where(x => x.Id == personalSubAttributeDto.Id).FirstOrDefault();

            if (personalSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to delete this personal sub attribute");
            }

            _dbContext.Entry(personalSubAttributeEntity).State = System.Data.Entity.EntityState.Deleted;

        }

        private void TransferInfo(PersonalSubAttributeDto dto, PersonalSubAttribute entity)
        {
            if (dto.IsNew)
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Alias = dto.Alias;

        }

    }

    public class UpdateAddressSubAttribute
    {

        private string _applicationUserId;

        private List<AddressSubAttributeDto> _addressSubAttributeDtoList;
        private PersonsAttribute _personsAttributeEntity;
        private ApplicationDbContext _dbContext;

        public UpdateAddressSubAttribute(ApplicationDbContext dbContext, PersonsAttribute personsAttributeEntity, List<AddressSubAttributeDto> addressSubAttributeDtoList, string applicationUserId)
        {
            _addressSubAttributeDtoList = addressSubAttributeDtoList;
            _personsAttributeEntity = personsAttributeEntity;
            _applicationUserId = applicationUserId;
            _dbContext = dbContext;
        }

        public void PerformAction()
        {
            _addressSubAttributeDtoList.Where(x => x.IsNew).ToList().ForEach(x => Add(x));
            _addressSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Update).ToList().ForEach(x => Update(x));
            _addressSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Delete).ToList().ForEach(x => Delete(x));

        }

        private void Add(AddressSubAttributeDto addressSubAttributeDto)
        {
            var addressSubAttributeEntity = new AddressSubAttribute();
            TransferInfo(addressSubAttributeDto, addressSubAttributeEntity);

            _personsAttributeEntity.AddressSubAttribute.Add(addressSubAttributeEntity);

            var addressSubAttribute = new UpdateAddressSubAttribute(_dbContext, _personsAttributeEntity, _addressSubAttributeDtoList, _applicationUserId);

        }

        private void Update(AddressSubAttributeDto addressSubAttributeDto)
        {

            var addressSubAttributeEntity = _personsAttributeEntity.AddressSubAttribute.Where(x => x.Id == addressSubAttributeDto.Id).FirstOrDefault();

            if (addressSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to update this address sub attribute");
            }

            TransferInfo(addressSubAttributeDto, addressSubAttributeEntity);

            _dbContext.Entry(addressSubAttributeEntity).State = System.Data.Entity.EntityState.Modified;

        }

        private void Delete(AddressSubAttributeDto addressSubAttributeDto)
        {

            var addressSubAttributeEntity = _personsAttributeEntity.AddressSubAttribute.Where(x => x.Id == addressSubAttributeDto.Id).FirstOrDefault();

            if (addressSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to delete this address sub attribute");
            }

            _dbContext.Entry(addressSubAttributeEntity).State = System.Data.Entity.EntityState.Deleted;

        }

        private void TransferInfo(AddressSubAttributeDto dto, AddressSubAttribute entity)
        {
            if (dto.IsNew)
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            entity.StreetAddress = dto.StreetAddress;
            entity.StreetAddress1 = dto.StreetAddress1;
            entity.City = dto.City;
            entity.State = dto.State;
            entity.PostCode = dto.PostCode;
            entity.CountryRegion = dto.CountryRegion;

        }

    }

    public class UpdatePhoneNumberSubAttribute
    {

        private string _applicationUserId;

        private List<PhoneNumberSubAttributeDto> _phoneNumberSubAttributeDtoList;
        private PersonsAttribute _personsAttributeEntity;
        private ApplicationDbContext _dbContext;

        public UpdatePhoneNumberSubAttribute(ApplicationDbContext dbContext, PersonsAttribute personsAttributeEntity, List<PhoneNumberSubAttributeDto> phoneNumberSubAttributeDtoList, string applicationUserId)
        {
            _phoneNumberSubAttributeDtoList = phoneNumberSubAttributeDtoList;
            _personsAttributeEntity = personsAttributeEntity;
            _applicationUserId = applicationUserId;
            _dbContext = dbContext;
        }

        public void PerformAction()
        {
            _phoneNumberSubAttributeDtoList.Where(x => x.IsNew).ToList().ForEach(x => Add(x));
            _phoneNumberSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Update).ToList().ForEach(x => Update(x));
            _phoneNumberSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Delete).ToList().ForEach(x => Delete(x));

        }

        private void Add(PhoneNumberSubAttributeDto phoneNumberSubAttributeDto)
        {
            var phoneSubAttributeEntity = new PhoneNumberSubAttribute();
            TransferInfo(phoneNumberSubAttributeDto, phoneSubAttributeEntity);

            _personsAttributeEntity.PhoneNumberSubAttribute.Add(phoneSubAttributeEntity);

            //update subAttrbutes (
        }

        private void Update(PhoneNumberSubAttributeDto phoneNumberSubAttributeDto)
        {

            var phoneNumberSubAttributeEntity = _personsAttributeEntity.PhoneNumberSubAttribute.Where(x => x.Id == phoneNumberSubAttributeDto.Id).FirstOrDefault();

            if (phoneNumberSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to update this phone number sub attribute");
            }

            TransferInfo(phoneNumberSubAttributeDto, phoneNumberSubAttributeEntity);

            _dbContext.Entry(phoneNumberSubAttributeEntity).State = System.Data.Entity.EntityState.Modified;

        }

        private void Delete(PhoneNumberSubAttributeDto phoneNumberSubAttributeDto)
        {

            var phoneNumberSubAttributeEntity = _personsAttributeEntity.PhoneNumberSubAttribute.Where(x => x.Id == phoneNumberSubAttributeDto.Id).FirstOrDefault();

            if (phoneNumberSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to delete this address sub attribute");
            }

            _dbContext.Entry(phoneNumberSubAttributeEntity).State = System.Data.Entity.EntityState.Deleted;

        }

        private void TransferInfo(PhoneNumberSubAttributeDto dto, PhoneNumberSubAttribute entity)
        {
            if (dto.IsNew)
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            entity.Ext = dto.Ext;
            entity.Number = dto.Number;

        }

    }

    public class UpdateTimeFrameSubAttribute
    {

        private string _applicationUserId;

        private List<TimeFrameSubAttributeDto> _timeFrameSubAttributeDtoList;
        private PersonsAttribute _personsAttributeEntity;
        private ApplicationDbContext _dbContext;

        public UpdateTimeFrameSubAttribute(ApplicationDbContext dbContext, PersonsAttribute personsAttributeEntity, List<TimeFrameSubAttributeDto> timeFrameSubAttributeDtoList, string applicationUserId)
        {
            _timeFrameSubAttributeDtoList = timeFrameSubAttributeDtoList;
            _personsAttributeEntity = personsAttributeEntity;
            _applicationUserId = applicationUserId;
            _dbContext = dbContext;
        }

        public void PerformAction()
        {
            _timeFrameSubAttributeDtoList.Where(x => x.IsNew).ToList().ForEach(x => Add(x));
            _timeFrameSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Update).ToList().ForEach(x => Update(x));
            _timeFrameSubAttributeDtoList.Where(x => x.ModifyActionRequired == ControllerHelper.Enumerations.ModifyActionRequired.Delete).ToList().ForEach(x => Delete(x));

        }

        private void Add(TimeFrameSubAttributeDto timeFrameSubAttributeDto)
        {
            var timeFrameSubAttributeEntity = new TimeFrameSubAttribute();
            TransferInfo(timeFrameSubAttributeDto, timeFrameSubAttributeEntity);

            _personsAttributeEntity.TimeFrameSubAttribute.Add(timeFrameSubAttributeEntity);

            //update subAttrbutes (
        }

        private void Update(TimeFrameSubAttributeDto timeFrameSubAttributeDto)
        {

            var timeFrameSubAttributeEntity = _personsAttributeEntity.TimeFrameSubAttribute.Where(x => x.Id == timeFrameSubAttributeDto.Id).FirstOrDefault();

            if (timeFrameSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to update this time frame sub attribute");
            }

            TransferInfo(timeFrameSubAttributeDto, timeFrameSubAttributeEntity);

            _dbContext.Entry(timeFrameSubAttributeEntity).State = System.Data.Entity.EntityState.Modified;

        }

        private void Delete(TimeFrameSubAttributeDto timeFrameSubAttributeDto)
        {

            var timeFrameSubAttributeEntity = _personsAttributeEntity.TimeFrameSubAttribute.Where(x => x.Id == timeFrameSubAttributeDto.Id).FirstOrDefault();

            if (timeFrameSubAttributeEntity == null)
            {
                throw new Exception("User is not authorised to delete this time frame sub attribute");
            }

            _dbContext.Entry(timeFrameSubAttributeEntity).State = System.Data.Entity.EntityState.Deleted;

        }

        private void TransferInfo(TimeFrameSubAttributeDto dto, TimeFrameSubAttribute entity)
        {
            if(dto.IsNew)
            {
                entity.Id = Guid.NewGuid().ToString();
            }

            entity.From = dto.From;
            entity.To = dto.To;

        }

    }
}