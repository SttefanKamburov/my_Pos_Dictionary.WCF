using Dictionary_WCF.Shared.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using Dictionary_WCF.AppConstanst;

namespace Dictionary_WCF
{
    public class AddressService : IAddressService
    {
        public IEnumerable<AddressDTO> GetAll()
        {
            List<AddressDTO> addresses = new List<AddressDTO>();
            using (SqlConnection conn = new SqlConnection(AppConstanst.AppConstants.CONNECTION_STRING))
            {
                var command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM DictionaryDB.dbo.Addresses";
                conn.Open();
                using (var reader = command.ExecuteReader())
                {
                    bool IsHomeBuff = false;
                    while (reader.Read())
                    {
                        if (reader.GetValue(reader.GetOrdinal("IsHomeAddress")).ToString().Equals("True"))
                        {
                            IsHomeBuff = true;
                        }
                        else IsHomeBuff = false;
                        addresses.Add(new AddressDTO
                        {
                            Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("AddressId")).ToString()),
                            IsHomeAddress = IsHomeBuff,
                            PersonId = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString()),
                            Address = reader.GetValue(reader.GetOrdinal("Address")).ToString()
                        });
                    }
                }
            }

            return addresses;
        }

        public AddressDTO GetAddress(string id)
        {
            if (ValidateAddress(id))
            {
                AddressDTO address = new AddressDTO();
                using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                {
                    var command = conn.CreateCommand();
                    command.CommandText = "SELECT * FROM DictionaryDB.dbo.Addresses WHERE AddressId = @id";
                    command.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        bool IsHomeBuff = false;
                        while (reader.Read())
                        {
                            if (reader.GetValue(reader.GetOrdinal("IsHomeAddress")).ToString().Equals("True"))
                            {
                                IsHomeBuff = true;
                            }
                            else IsHomeBuff = false;

                            address.Id = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("AddressId")).ToString());
                            address.IsHomeAddress = IsHomeBuff;
                            address.PersonId = Int32.Parse((string)reader.GetValue(reader.GetOrdinal("PersonId")).ToString());
                            address.Address = reader.GetValue(reader.GetOrdinal("Address")).ToString();
                        }
                    }
                }

                return address;
            }
            throw new Exception("Address not found");
        }

        public AddressDTO AddAddress(AddressDTO newAddress)
        {
            if (!ValidateAddress(newAddress.Id.ToString()))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", newAddress.Id);
                        cmd.Parameters.AddWithValue("@personid", newAddress.PersonId);
                        cmd.Parameters.AddWithValue("@address", newAddress.Address);
                        cmd.Parameters.AddWithValue("@ishome", newAddress.IsHomeAddress);
                        cmd.CommandText = "INSERT INTO DictionaryDB.dbo.Addresses (AddressId,PersonId,Address,IsHomeAddress) VALUES (@id,@personid,@address,@ishome)";
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }

                return newAddress;
            }

            throw new Exception("Address already exists !");
        }

        public void DeleteAddress(string id)
        {
            if (ValidateAddress(id))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.CommandText = string.Format("DELETE FROM DictionaryDB.dbo.Addresses WHERE AddressId = @id");
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
                throw new Exception("Address for deletion not found !");
            }
        }

        public AddressDTO UpdateAddress(AddressDTO newAddress)
        {
            if (ValidateAddress(newAddress.Id.ToString()))
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
                    {
                        var cmd = conn.CreateCommand();
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", newAddress.Id);
                        cmd.Parameters.AddWithValue("@personid", newAddress.PersonId);
                        cmd.Parameters.AddWithValue("@address", newAddress.Address);
                        cmd.Parameters.AddWithValue("@ishome", newAddress.IsHomeAddress);
                        cmd.CommandText = string.Format("UPDATE DictionaryDB.dbo.Addresses SET PersonId = @personid, Address = @address,IsHomeAddress = @ishome WHERE AddressId = @id");
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw (e);
                }

                return newAddress;
            }

            throw new Exception("Address for update not found !");
        }

        public bool ValidateAddress(string id)
        {
            using (SqlConnection conn = new SqlConnection(AppConstants.CONNECTION_STRING))
            {
                var cmd = conn.CreateCommand();
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = "SELECT COUNT(Addresses.AddressId) FROM Addresses WHERE Addresses.AddressId = @id;";
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
