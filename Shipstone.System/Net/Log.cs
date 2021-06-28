using System;
using System.Net;

namespace Shipstone.System.Net
{
    /// <summary>
    /// Represents a server log file conforming to the NCSA Common log format.
    /// </summary>
    public struct Log : IEquatable<Log>
    {
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
        public Log(String host, String identity, String authUser, String request, HttpStatusCode status, int bytes)
        {
            if (bytes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof (bytes));
            }

            DateTime utcNow = DateTime.UtcNow;
            this.AuthUser = String.IsNullOrWhiteSpace(authUser) ? null : authUser;
            this.Bytes = bytes;
            this.Date = utcNow.ToLocalTime();
            this.Host = String.IsNullOrWhiteSpace(host) ? null : host;
            this.Identity = String.IsNullOrWhiteSpace(identity) ? null : identity;
            this.Request = request;
            this.Status = status;
            this.UtcDate = utcNow;
        }

        public bool Equals(Log log) => throw new NotImplementedException();

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

        public override int GetHashCode() => throw new NotImplementedException();
        public override String ToString() => throw new NotImplementedException();
        public String ToString(String dateFormat) => throw new NotImplementedException();

        public static Log Parse(String s) => throw new NotImplementedException();

        public static bool operator ==(Log logA, Log logB) => logA.Equals(logB);
        public static bool operator !=(Log logA, Log logB) => !logA.Equals(logB);
    }
}