
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dictionary_WCF.AppConstanst;
using Dictionary_WCF.Shared.ModelsDTO;
using Dictionary_WCF.Shared.OutputModels;
using Dictionary_WCF.Mappers;
using System.Linq;

namespace Dictionary_WCF
{
    public class PeopleService : IPeopleService
    {
        public PersonDTO GetPerson(string id)
        {
            if (ValidatePerson(id))
            {
                var person = new PersonDTO();
                using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                {
                    SqlCommand command = conn.CreateCommand();
                    command.Parameters.AddWithValue("@id", id);
                    command.CommandText = "SELECT * FROM DictionaryDB.dbo.People WHERE PersonId = @id";
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            person.Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString());
                            person.Name = reader.GetValue(reader.GetOrdinal("Name")).ToString();
                            person.MiddleName = reader.GetValue(reader.GetOrdinal("Middlename")).ToString();
                            person.Surname = reader.GetValue(reader.GetOrdinal("Surname")).ToString();
                            person.PhoneNumbers = GetPhoneNumbersById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()));
                            person.Addresses = GetAddressesById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()));
                        }
                    }
                }

                return person;
            }

            throw new Exception("Person not found !");
        }

        IEnumerable<PersonDTO> IPeopleService.GetAll()
        {
            PersonDTO buff = new PersonDTO();
            List<PersonDTO> people = new List<PersonDTO>();
            using (SqlConnection conn = new SqlConnection(AppConstanst.AppConstants.CONNECTION_STRING))
            {
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM DictionaryDB.dbo.People;";
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        people.Add(new PersonDTO
                        {
                            Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()),
                            Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                            MiddleName = reader.GetValue(reader.GetOrdinal("Middlename")).ToString(),
                            Surname = reader.GetValue(reader.GetOrdinal("Surname")).ToString(),
                            PhoneNumbers = GetPhoneNumbersById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString())),
                            Addresses = GetAddressesById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()))
                        }); ;
                    }
                }
            }

            return people;
        }

        List<PhoneNumberDTO> GetPhoneNumbersById(string userId)
        {
            List<PhoneNumberDTO> phones = new List<PhoneNumberDTO>();
            using (SqlConnection conn = new SqlConnection(AppConstanst.AppConstants.CONNECTION_STRING))
            {
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM DictionaryDB.dbo.PhoneNumbers WHERE PersonId = @id"; ;
                command.Parameters.AddWithValue("@id", userId);
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    bool IsHomeBuff = false;
                    while (reader.Read())
                    {
                        string asd = reader.GetValue(reader.GetOrdinal("IsHome")).ToString();
                        if (reader.GetValue(reader.GetOrdinal("IsHome")).ToString().Equals("True"))
                        {
                            IsHomeBuff = true;
                        }
                        else IsHomeBuff = false;
                        phones.Add(new PhoneNumberDTO
                        {
                            Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PhoneNumberId")).ToString()),
                            PersonId = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()),
                            Number = reader.GetValue(reader.GetOrdinal("Number")).ToString(),
                            IsHome = IsHomeBuff
                        });
                    }
                }
            }

            return phones;
        }

        List<AddressDTO> GetAddressesById(string userId)
        {
            List<AddressDTO> phones = new List<AddressDTO>();
            using (SqlConnection conn = new SqlConnection(AppConstanst.AppConstants.CONNECTION_STRING))
            {
                var command = conn.CreateCommand();
                command.Parameters.AddWithValue("@id", userId);
                command.CommandText = "SELECT * FROM DictionaryDB.dbo.Addresses WHERE PersonId = @id;";
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    bool IsHomeBuff = false;
                    while (reader.Read())
                    {
                        if (reader.GetValue(reader.GetOrdinal("IsHomeAddress")).ToString().Equals("1"))
                        {
                            IsHomeBuff = true;
                        }
                        else IsHomeBuff = false;
                        phones.Add(new AddressDTO
                        {
                            Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("AddressId")).ToString()),
                            IsHomeAddress = IsHomeBuff,
                            PersonId = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()),
                            Address = reader.GetValue(reader.GetOrdinal("Address")).ToString()
                        });
                    }
                }
            }

            return phones;
        }

        public void DeletePerson(string id)
        {
            if (ValidatePerson(id))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        cmd.Parameters.AddWithValue("@id", id);
                        conn.Open();
                        cmd.CommandText = String.Format("DELETE FROM DictionaryDB.dbo.People WHERE PersonId = @id;");
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            else
            {
                throw new Exception("Person for deletion found !");
            }
        }

        public PersonDTO AddPerson(PersonDTO newPerson)
        {
            if (!ValidatePerson(newPerson.Id.ToString()))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", newPerson.Id);
                        cmd.Parameters.AddWithValue("@name", newPerson.Name);
                        cmd.Parameters.AddWithValue("@middlename", newPerson.MiddleName);
                        cmd.Parameters.AddWithValue("@surname", newPerson.Surname);
                        cmd.CommandText = string.Format("INSERT INTO DictionaryDB.dbo.People (PersonId,Name,MiddleName,Surname) VALUES (@id,@name,@middlename,@surname)");
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
                return newPerson;
            }

            throw new Exception("Person already exists !");
        }

        public PersonDTO UpdatePerson(PersonDTO person)
        {
            if (ValidatePerson(person.Id.ToString()))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", person.Id);
                        cmd.Parameters.AddWithValue("@name", person.Name);
                        cmd.Parameters.AddWithValue("@middlename", person.MiddleName);
                        cmd.Parameters.AddWithValue("@surname", person.Surname);
                        cmd.CommandText = string.Format("UPDATE DictionaryDB.dbo.People SET Name = @name, MiddleName = @middlename,Surname = @surname WHERE PersonId = @id");
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }

                return person;
            }

            throw new Exception("Person for upadte not found !");
        }

        public List<PersonDTO> GetPersonByCriteria(PersonCriteriaInputModel model)
        {
            List<PersonDTO> people = new List<PersonDTO>();
            List<PersonOutputModel> outputModels = new List<PersonOutputModel>();

            if (!String.IsNullOrEmpty(model.Name) || !String.IsNullOrEmpty(model.MiddleName) || !string.IsNullOrEmpty(model.Surname) || model.PersonId != null)
            {
                using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                {
                    var command = conn.CreateCommand();
                    if (model.PersonId == null)
                    {
                        model.PersonId = 0;
                    }
                    command.CommandText = "SELECT * FROM DictionaryDB.dbo.People WHERE PersonId = @id OR Name = @name OR Middlename = @middlename OR Surname = @surname;";
                    command.Parameters.AddWithValue("@id", model.PersonId);
                    command.Parameters.AddWithValue("@name", model.Name);
                    command.Parameters.AddWithValue("@middlename", model.MiddleName);
                    command.Parameters.AddWithValue("@surname", model.Surname);
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            people.Add(new PersonDTO
                            {
                                Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()),
                                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                                MiddleName = reader.GetValue(reader.GetOrdinal("Middlename")).ToString(),
                                Surname = reader.GetValue(reader.GetOrdinal("Surname")).ToString(),
                                PhoneNumbers = GetPhoneNumbersById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString())),
                                Addresses = GetAddressesById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()))
                            });
                        }
                    }
                }
            }

            if (model.AddressId != null)
            {
                if (people.Any())
                {
                    people = (List<PersonDTO>)people
                        .Where(p => p.Addresses.Any(a => a.Id == model.AddressId));
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var command = conn.CreateCommand();
                        command.CommandText =
                            "SELECT People.PersonId, People.Name, People.Middlename, People.Surname " +
                            "FROM People INNER JOIN Addresses " +
                            "ON People.PersonId = Addresses.PersonId WHERE Addresses.AddressId = @id;";
                        command.Parameters.AddWithValue("@id", model.AddressId);
                        conn.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                people.Add((GetPerson((reader.GetValue(reader.GetOrdinal("PersonId")).ToString()))));
                            }
                        }
                    }
                }
            }

            if (model.PhoneNumberId != null)
            {
                if (people.Any())
                {
                    people = (List<PersonDTO>)people
                        .Where(p => p.PhoneNumbers.Any(x => x.Id == model.PhoneNumberId));
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var command = conn.CreateCommand();
                        command.CommandText =
                        "SELECT People.PersonId " +
                        "FROM People INNER JOIN PhoneNumbers " +
                        "ON People.PersonId = PhoneNumbers.PersonId WHERE PhoneNumbers.PhoneNumberId = @id;";
                        command.Parameters.AddWithValue("@id", model.PhoneNumberId);
                        conn.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                people.Add(GetPerson((reader.GetValue(reader.GetOrdinal("PersonId")).ToString())));
                            }
                        }
                    }
                }
            }

            return people;
        }

        public bool ValidatePerson(string id)
        {
            using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
            {
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = "SELECT COUNT(People.PersonId) FROM People WHERE People.PersonId = @id;";
                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<string> GetPeopleByOutputModel()
        {
            PersonDTO buff = new PersonDTO();
            List<PersonDTO> people = new List<PersonDTO>();
            List<string> output = new List<string>();
            using (SqlConnection conn = new SqlConnection(AppConstanst.AppConstants.CONNECTION_STRING))
            {
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM DictionaryDB.dbo.People;";
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        people.Add(new PersonDTO
                        {
                            Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()),
                            Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                            MiddleName = reader.GetValue(reader.GetOrdinal("Middlename")).ToString(),
                            Surname = reader.GetValue(reader.GetOrdinal("Surname")).ToString(),
                            PhoneNumbers = GetPhoneNumbersById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString())),
                            Addresses = GetAddressesById(((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()))
                        }); ;
                    }
                }
            }

            string toAdd = "";
            List<PersonOutputModel> outputModels = new List<PersonOutputModel>();
            foreach (PersonDTO p in people) 
            {
                outputModels.Add(PersonMapper.SelectPersonOutputModelFromPersonDTO(p));
            }

            foreach (PersonOutputModel p in outputModels) 
            {
                toAdd = "Name: " + p.Name + Environment.NewLine;
                foreach (AddressOutputModel officeAddress in p.OfficeAdresses) 
                {
                    toAdd +="Office Address : "+ officeAddress.Address + Environment.NewLine;
                }
                foreach (PhoneNumberOutputModel officePhones in p.OfficePhoneNumbers) 
                {
                    toAdd += officePhones.PhoneNumber + Environment.NewLine;
                }
                foreach (AddressOutputModel homeAddresses in p.HomeAddresses) 
                {
                    toAdd +="Home Address : "+ homeAddresses.Address + Environment.NewLine;
                }
                foreach (PhoneNumberOutputModel homePhoneNumbers in p.HomePhoneNumbers) 
                {
                    toAdd += homePhoneNumbers.PhoneNumber + Environment.NewLine;
                }

                output.Add(toAdd);
            }

            return output;
        }
    }
}