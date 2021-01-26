using Dictionary_WCF.Shared.ModelsDTO;
using Dictionary_WCF.AppConstanst;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Dictionary_WCF
{
    public class PhoneNumberService : IPhoneNumberService
    {
        public PhoneNumberDTO AddPhone(PhoneNumberDTO newPhone)
        {
            if (!ValidatePhoneNumber(newPhone.Id.ToString()))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", newPhone.Id);
                        cmd.Parameters.AddWithValue("@personid", newPhone.PersonId);
                        cmd.Parameters.AddWithValue("@number", newPhone.Number);
                        cmd.Parameters.AddWithValue("@ishome", newPhone.IsHome);
                        cmd.CommandText = "INSERT INTO DictionaryDB.dbo.PhoneNumbers (PhoneNumberId,PersonId,Number,IsHome) VALUES (@id,@personid,@number,@ishome)";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }

                return newPhone;
            }

            throw new Exception("Phone already exists !");
        }

        public void DeletePhoneNumber(string id)
        {
            if (ValidatePhoneNumber(id))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.CommandText = "DELETE FROM DictionaryDB.dbo.PhoneNumbers WHERE PhoneNumberId = @id";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }
            }
            else throw new Exception("Phone for deletion not found ! ");
        }

        public IEnumerable<PhoneNumberDTO> GetAll()
        {
            List<PhoneNumberDTO> phones = new List<PhoneNumberDTO>();

            using (SqlConnection conn = new SqlConnection(AppConstanst.AppConstants.CONNECTION_STRING))
            {
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM DictionaryDB.dbo.PhoneNumbers";
                bool IsHomeBuff = false;
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(reader.GetOrdinal("IsHome")).ToString().Equals("True"))
                        {
                            IsHomeBuff = true;
                        }
                        else IsHomeBuff = false;
                        phones.Add(new PhoneNumberDTO
                        {
                            Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PhoneNumberId")).ToString()),
                            PersonId = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()),
                            IsHome = IsHomeBuff,
                            Number = reader.GetValue(reader.GetOrdinal("Number")).ToString()
                        });
                    }
                }
            }

            return phones;
        }

        public PhoneNumberDTO GetPhoneNumberById(string id)
        {
            if (ValidatePhoneNumber(id))
            {
                PhoneNumberDTO phoneNumber = new PhoneNumberDTO();
                using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                {
                    var command = conn.CreateCommand();
                    command.CommandText = "SELECT * FROM DictionaryDB.dbo.PhoneNumbers WHERE PhoneNumberId = @id";
                    command.Parameters.AddWithValue("@id", id);
                    bool IsHomeBuff = false;
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetValue(reader.GetOrdinal("IsHome")).ToString().Equals("True"))
                            {
                                IsHomeBuff = true;
                            }
                            else { IsHomeBuff = false; }
                            phoneNumber.IsHome = IsHomeBuff;
                            phoneNumber.Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PhoneNumberId")).ToString());
                            phoneNumber.PersonId = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString());
                            phoneNumber.Number = reader.GetValue(reader.GetOrdinal("Number")).ToString();
                        }
                    }

                    return phoneNumber;
                }
            }

            throw new ArgumentNullException("Phone number not found");
        }

        public PhoneNumberDTO UpdatePhone(PhoneNumberDTO newPhone)
        {
            if (ValidatePhoneNumber(newPhone.Id.ToString()))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", newPhone.Id);
                        cmd.Parameters.AddWithValue("@personid", newPhone.PersonId);
                        cmd.Parameters.AddWithValue("@number", newPhone.Number);
                        cmd.Parameters.AddWithValue("@ishome", newPhone.IsHome);
                        cmd.CommandText = string.Format("UPDATE DictionaryDB.dbo.PhoneNumbers SET PersonId = @personid, IsHome = @ishome, Number = @number WHERE PhoneNumberId = @id");
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }

                return newPhone;
            }

            throw new Exception("Phone for update not found !");
        }

        public bool ValidatePhoneNumber(string id)
        {
            using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
            {
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = "SELECT COUNT(PhoneNumbers.PhoneNumberId) FROM PhoneNumbers WHERE PhoneNumbers.PhoneNumberId = @id;";
                Int32 count = (Int32)cmd.ExecuteScalar();
                if (count > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
