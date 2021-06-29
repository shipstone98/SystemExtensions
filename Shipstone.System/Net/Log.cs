using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Shipstone.System.Net
{
    /// <summary>
    /// Represents a server log file conforming to the NCSA Common log format.
    /// </summary>
    public struct Log : IEquatable<Log>
    {
        private readonly int _HashCode;

        /// <summary>
        /// Gets the ID of the user requesting the resource.
        /// </summary>
        /// <value>The ID of the user requesting the resource.</value>
        public String AuthUser { get; }

        /// <summary>
        /// Gets the size of the object returned to the client, measured in bytes.
        /// </summary>
        /// <value>The size of the object returned to the client, measured in bytes.</value>
        public int Bytes { get; }

        /// <summary>
        /// Gets the local timestamp at the time the request was received.
        /// </summary>
        /// <value>The local timestamp at the time the request was received.</value>
        public DateTime Date { get; }

        /// <summary>
        /// Gets the IP address of the remote client which made the request.
        /// </summary>
        /// <value>The IP address of the remote client which made the request.</value>
        public String Host { get; }

        /// <summary>
        /// Gets the RFC 1413 identity of the client.
        /// </summary>
        /// <value>The RFC 1413 identity of the client. This is usually <c>null</c>.</value>
        public String Identity { get; }

        /// <summary>
        /// Gets the request from the client.
        /// </summary>
        /// <value>The request from the client.</value>
        public String Request { get; }

        /// <summary>
        /// Gets the <see cref="HttpStatusCode" /> returned to the client.
        /// </summary>
        /// <value>The <see cref="HttpStatusCode" /> returned to the client.</value>
        public HttpStatusCode Status { get; }

        /// <summary>
        /// Gets the UTC timestamp at the time the request was received.
        /// </summary>
        /// <value>The UTC timestamp at the time the request was received.</value>
        public DateTime UtcDate { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log" /> structure containing the specified fields and current timestamp.
        /// </summary>
        /// <param name="host">The IP address of the remote client which made the request.</param>
        /// <param name="identity">The RFC 1413 identity of the client. This is usually <c>null</c>.</param>
        /// <param name="authUser">The ID of the user requesting the resource.</param>
        /// <param name="request">The request from the client.</param>
        /// <param name="status">The <see cref="HttpStatusCode" /> returned to the client.</param>
        /// <param name="bytes">The size of the object returned to the client, measured in bytes.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="bytes" /></c> is less than 0.</exception>
        public Log(String host, String identity, String authUser, String request, HttpStatusCode status, int bytes) : this(host, identity, authUser, DateTime.Now, request, status, bytes) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log" /> structure containing the specified fields and timestamp.
        /// </summary>
        /// <param name="host">The IP address of the remote client which made the request.</param>
        /// <param name="identity">The RFC 1413 identity of the client. This is usually <c>null</c>.</param>
        /// <param name="authUser">The ID of the user requesting the resource.</param>
        /// <param name="date">The timestamp at the time the request was received.</param>
        /// <param name="request">The request from the client.</param>
        /// <param name="status">The <see cref="HttpStatusCode" /> returned to the client.</param>
        /// <param name="bytes">The size of the object returned to the client, measured in bytes.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="bytes" /></c> is less than 0.</exception>
        public Log(String host, String identity, String authUser, DateTime date, String request, HttpStatusCode status, int bytes)
        {
            if (bytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof (bytes));
            }

            this._HashCode = 0;
            this.AuthUser = String.IsNullOrWhiteSpace(authUser) ? null : authUser;
            this.Bytes = bytes;
            this.Date = date;
            this.Host = String.IsNullOrWhiteSpace(host) ? null : host;
            this.Identity = String.IsNullOrWhiteSpace(identity) ? null : identity;
            this.Request = request;
            this.Status = status;
            this.UtcDate = date.ToUniversalTime();
            this._HashCode = this.GetHashCode();
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified <see cref="Log" />.
        /// </summary>
        /// <param name="log">The <see cref="Log" /> to compare to this instance.</param>
        /// <returns><c>true</c> if <c><paramref name="log" /></c> equals the value of this instance; otherwise, <c>false</c>.</returns>
        public bool Equals(Log log) => String.Equals(this.AuthUser, log.AuthUser) && this.Bytes == log.Bytes && String.Equals(this.Host, log.Host) && String.Equals(this.Identity, log.Identity) && String.Equals(this.Request, log.Request) && this.Status == log.Status && this.UtcDate.Equals(log.UtcDate);

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare to this instance.</param>
        /// <returns><c>true</c> if <c><paramref name="obj" /></c> is an instance of <see cref="Log" /> and equals the value of this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(Object obj)
        {
            try
            {
                return this.Equals((Log) obj);
            }

            catch (InvalidCastException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            if (this._HashCode == 0)
            {
                int hashCode = 31;
                hashCode ^= this.AuthUser is null ? 0 : this.AuthUser.GetHashCode();
                hashCode ^= this.Bytes;
                hashCode ^= this.Host is null ? 0 : this.Host.GetHashCode();
                hashCode ^= this.Identity is null ? 0 : this.Identity.GetHashCode();
                hashCode ^= this.Request is null ? 0 : this.Request.GetHashCode();
                hashCode ^= (int) this.Status;
                hashCode ^= this.UtcDate.GetHashCode();
                return hashCode;
            }

            return this._HashCode;
        }

        /// <summary>
        /// Returns the string representation of the current <see cref="Log" /> object using the default timestamp formatting.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Log" /> object.</returns>
        public override String ToString() => this.ToString(null);

        /// <summary>
        /// Returns the string representation of the current <see cref="Log" /> object using the specified timestamp formatting.
        /// </summary>
        /// <param name="dateFormat">A standard or custom date and time format string, or <c>null</c> to use the default.</param>
        /// <returns>A string representation of the current <see cref="Log" /> object.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
        /// <exception cref="FormatException">The length of <c><paramref name="dateFormat" /></c> is 1, and it is not one of the format specified characters defined for <see cref="System.Globalization.DateTimeFormatInfo" /> -or- <c><paramref name="dateFormat" /></c> does not contain a valid custom format pattern.</exception>
        public String ToString(String dateFormat)
        {
            const String DEFAULT_FORMAT = "dd/MMM/yyyy HH:mm:ss zzz";
            const String NULL_PLACEHOLDER = "-";
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Host ?? NULL_PLACEHOLDER);
            sb.Append(' ');
            sb.Append(this.Identity ?? NULL_PLACEHOLDER);
            sb.Append(' ');
            sb.Append(this.AuthUser ?? NULL_PLACEHOLDER);
            sb.Append(" [");
            sb.Append(this.Date.ToString(dateFormat ?? DEFAULT_FORMAT));
            sb.Append("] ");

            if (this.Request is null)
            {
                sb.Append(NULL_PLACEHOLDER);
            }

            else
            {
                sb.Append('"');
                sb.Append(Regex.Replace(this.Request, @"\s+", " "));
                sb.Append('"');
            }

            sb.Append(' ');
            sb.Append((int) this.Status);
            sb.Append(' ');
            sb.Append(this.Bytes);
            return sb.ToString();
        }

        private static String GetToken(ref String str)
        {
            int index = str.IndexOf(' ');
            String substring;

            if (index == -1)
            {
                substring = str;
                str = String.Empty;
            }

            else
            {
                substring = str.Substring(0, index);
                str = str.Remove(0, index).TrimStart();
            }

            return substring == "-" ? null : substring;
        }

        /// <summary>
        /// Converts the string representation of a server log to an equivalent <see cref="Log" /> object.
        /// </summary>
        /// <param name="str">A string containing the server log to convert.</param>
        /// <returns>A <see cref="Log" /> instance whose value is represented by <c><paramref name="str" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="str" /></c> is <c>null</c>.</exception>
        /// <exception cref="FormatException"><c><paramref name="str" /></c> is not in the correct format.</exception>
        public static Log Parse(String str)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof (str));
            }

            try
            {
                str.TrimStart();
                String host = Log.GetToken(ref str);
                String ident = Log.GetToken(ref str);
                String authUser = Log.GetToken(ref str);

                if (str[0] != '[')
                {
                    throw new Exception();
                }

                str = str.Remove(0, 1);
                int index = str.IndexOf(']');

                if (index == -1)
                {
                    throw new Exception();
                }

                String dateString = str.Substring(0, index);
                str = str.Remove(0, index + 1).TrimStart();
                DateTime date = DateTime.Parse(dateString);

                if (str[0] != '"')
                {
                    throw new Exception();
                }

                str = str.Remove(0, 1);

                if ((index = str.IndexOf('"', 1)) == -1)
                {
                    throw new Exception();
                }
                
                String request = str.Substring(0, index);
                str = str.Remove(0, index + 1).TrimStart();
                HttpStatusCode status = (HttpStatusCode) Int32.Parse(Log.GetToken(ref str));

                if (!Enum.IsDefined(typeof (HttpStatusCode), status))
                {
                    throw new Exception();
                }

                int bytes = Int32.Parse(Log.GetToken(ref str));
                
                if (!String.IsNullOrWhiteSpace(str))
                {
                    throw new Exception();
                }

                return new Log(host, ident, authUser, date, request, status, bytes);
            }

            catch
            {
                throw new FormatException($"{nameof (str)} is not in the correct format.");
            }
        }

        /// <summary>
        /// Determines whether two specified <see cref="Log" /> instances have the same value.
        /// </summary>
        /// <param name="logA">The first <see cref="Log" /> to compare.</param>
        /// <param name="logB">The second <see cref="Log" /> to compare.</param>
        /// <returns><c>true</c> if the value of <c><paramref name="logA" /></c> is the same as the value of <c><paramref name="logB" /></c>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Log logA, Log logB) => logA.Equals(logB);

        /// <summary>
        /// Determines whether two specified <see cref="Log" /> instances have different values.
        /// </summary>
        /// <param name="logA">The first <see cref="Log" /> to compare.</param>
        /// <param name="logB">The second <see cref="Log" /> to compare.</param>
        /// <returns><c>true</c> if the value of <c><paramref name="logA" /></c> is the different to the value of <c><paramref name="logB" /></c>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Log logA, Log logB) => !logA.Equals(logB);
    }
}