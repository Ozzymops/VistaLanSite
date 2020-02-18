using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace VistaLanSite.Classes
{
    public class Queries
    {
        // Construct connection string
        private string ConnectionString = "Data Source='tcp:185.41.126.25,9145'; Database='DerixVISTALAN'; User Id='DerixMASTER'; Password='Qr!K08vS'";
        //private string ConnectionString = "Data Source='192.168.10.100'; Database='DerixVISTALAN'; User Id='DerixMASTER'; Password='Qr!K08vS';";

        /// <summary>
        /// Register a new participant into the DB.
        /// </summary>
        /// <param name="participant">Participant to be registered</param>
        /// <returns>Has the procedure succeeded or not?</returns>
        public bool RegisterParticipant(Participant participant)
        {
            using (SqlConnection _connection = new SqlConnection(ConnectionString))
            {
                SqlCommand _command = new SqlCommand("INSERT INTO [Participants] SELECT @FirstName, @LastName, @StudentNumber, @StudentClass, @PhoneNumber, @BringsConsole, @ConsoleDetails, @BringsComputer, @ComputerDetails, @HasPaid;", _connection);

                _command.Parameters.Add(new SqlParameter("@FirstName", participant.FirstName));
                _command.Parameters.Add(new SqlParameter("@LastName", participant.LastName));
                _command.Parameters.Add(new SqlParameter("@StudentNumber", participant.StudentNumber));
                _command.Parameters.Add(new SqlParameter("@StudentClass", participant.StudentClass));
                _command.Parameters.Add(new SqlParameter("@PhoneNumber", participant.PhoneNumber));
                _command.Parameters.Add(new SqlParameter("@BringsConsole", participant.BringsConsole));
                _command.Parameters.Add(new SqlParameter("@BringsComputer", participant.BringsComputer));           
                _command.Parameters.Add(new SqlParameter("@HasPaid", false));

                if (!String.IsNullOrEmpty(participant.ConsoleDetails))
                {
                    _command.Parameters.Add(new SqlParameter("@ConsoleDetails", participant.ConsoleDetails));
                }
                else
                {
                    _command.Parameters.Add(new SqlParameter("@ConsoleDetails", "-"));
                }

                if (!String.IsNullOrEmpty(participant.ComputerDetails))
                {
                    _command.Parameters.Add(new SqlParameter("@ComputerDetails", participant.ComputerDetails));
                }
                else
                {
                    _command.Parameters.Add(new SqlParameter("@ComputerDetails", "-"));
                }

                try
                {
                    _connection.Open();

                    int _rows = _command.ExecuteNonQuery();

                    if (_rows > 0)
                    {
                        return true;
                    }

                    return false;
                }
                catch (Exception _exception)
                {
                    Debug.WriteLine("!ERROR! " + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + " - " + _exception.ToString());
                    return false;
                }
            }
        }

        /// <summary>
        /// Retrieve all participants for overview.
        /// </summary>
        /// <param name="participantType">0 - all, 1 - unpaid, 2 - paid.</param>
        /// <returns>List of participants</returns>
        public List<Participant> RetrieveParticipants(int participantType)
        {
            using (SqlConnection _connection = new SqlConnection(ConnectionString))
            {
                SqlCommand _command = new SqlCommand();

                switch(participantType)
                {
                    case 1:
                        _command = new SqlCommand("SELECT * FROM [Participants] WHERE [Participants].[HasPaid] = 0 ORDER BY [Id]", _connection);
                        break;

                    case 2:
                        _command = new SqlCommand("SELECT * FROM [Participants] WHERE [Participants].[HasPaid] = 1 ORDER BY [Id]", _connection);
                        break;

                    default:
                        _command = new SqlCommand("SELECT * FROM [Participants] ORDER BY [Id]", _connection);
                        break;
                }

                List<Participant> ParticipantList = new List<Participant>();

                try
                {
                    _connection.Open();

                    using (SqlDataReader _reader = _command.ExecuteReader())
                    {
                        if (_reader.HasRows)
                        {
                            int _rows = 0;

                            while (_reader.Read())
                            {
                                Participant NewParticipant = new Participant
                                {
                                    Id = (int)_reader["Id"],
                                    FirstName = (string)_reader["FirstName"],
                                    LastName = (string)_reader["LastName"],
                                    StudentNumber = (string)_reader["StudentNumber"],
                                    StudentClass = (string)_reader["StudentClass"],
                                    PhoneNumber = (string)_reader["PhoneNumber"],
                                    BringsConsole = (bool)_reader["BringsConsole"],
                                    ConsoleDetails = (string)_reader["ConsoleDetails"],
                                    BringsComputer = (bool)_reader["BringsComputer"],
                                    ComputerDetails = (string)_reader["ComputerDetails"],
                                    HasPaid = (bool)_reader["HasPaid"]
                                };

                                ParticipantList.Add(NewParticipant);
                                _rows++;
                            }

                            return ParticipantList;
                        }
                    }

                    return null;
                }
                catch (Exception _exception)
                {
                    Debug.WriteLine("!ERROR! " + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + " - " + _exception.ToString());
                    return null;
                }
            }
        }

        /// <summary>
        /// Retrieve the amount of participants who are already registered.
        /// </summary>
        /// <returns>Amount of registered and paid participants</returns>
        public int RetrieveParticipantCount()
        {
            using (SqlConnection _connection = new SqlConnection(ConnectionString))
            {
                SqlCommand _command = new SqlCommand("SELECT COUNT([Id]) FROM [Participants]", _connection);

                try
                {
                    _connection.Open();
                    int _count = (int)_command.ExecuteScalar();
                    return _count;
                }
                catch (Exception _exception)
                {
                    Debug.WriteLine("!ERROR! " + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + " - " + _exception.ToString());
                    return -1;
                }
            }
        }

        /// <summary>
        /// Update the paid status of a participant.
        /// </summary>
        /// <param name="UpdatedParticipantId">ID of to be updated participant</param>
        /// <returns>Has the procedure succeeded or not?</returns>
        public bool UpdateParticipantStatus(int UpdatedParticipantId)
        {
            using (SqlConnection _connection = new SqlConnection(ConnectionString))
            {
                SqlCommand _command = new SqlCommand("UPDATE [Participants] SET [HasPaid] = @HasPaid WHERE [Id] = @Id", _connection);

                _command.Parameters.Add(new SqlParameter("@HasPaid", 1));
                _command.Parameters.Add(new SqlParameter("@Id", UpdatedParticipantId));

                try
                {
                    _connection.Open();

                    int _rows = _command.ExecuteNonQuery();

                    if (_rows > 0)
                    {
                        return true;
                    }

                    return false;
                }
                catch (Exception _exception)
                {
                    Debug.WriteLine("!ERROR! " + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + " - " + _exception.ToString());
                    return false;
                }
            }
        }

        /// <summary>
        /// Delete a participant from the list.
        /// </summary>
        /// <param name="DeletedParticipantId">ID of to be deleted participant</param>
        /// <returns>Has the procedure succeeded or not?</returns>
        public bool DeleteParticipant(int DeletedParticipantId)
        {
            using (SqlConnection _connection = new SqlConnection(ConnectionString))
            {
                SqlCommand _command = new SqlCommand("DELETE FROM [Participants] WHERE [Id] = @Id", _connection);

                _command.Parameters.Add(new SqlParameter("@Id", DeletedParticipantId));

                try
                {
                    _connection.Open();

                    int _rows = _command.ExecuteNonQuery();

                    if (_rows > 0)
                    {
                        return true;
                    }

                    return false;
                }
                catch (Exception _exception)
                {
                    Debug.WriteLine("!ERROR! " + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + " - " + _exception.ToString());
                    return false;
                }
            }
        }
    }
}
