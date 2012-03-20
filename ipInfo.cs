using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;

namespace ExtensionMethods
{
    /// <summary>
    /// Wrapper class to simplify getting Network IP information.
    /// </summary>
    public class IPInfo
    {	
        protected string _interfaceName;
        protected string _ipAddressV4;
        protected string _ipAddressV6;
        protected Collection<string> _dnsServers;
        protected Collection<string> _gateways;
        protected string _hostname;		
        protected string _status;

        protected IPInfo(string interfaceName, string ipAddressV4, string ipAddressV6, Collection<string> dnsServers, Collection<string> gateways, string hostname, string status)
        {
            _interfaceName = interfaceName;
            _ipAddressV4 = ipAddressV4;
            _ipAddressV6 = ipAddressV6;
            _dnsServers = dnsServers;
            _gateways = gateways;
            _hostname = hostname;
            _status = status;
        }
		
        /// <summary>
        /// The name of the current network interface.
        /// </summary>
        public string InterfaceName {
            get { return _interfaceName; }
        }
		
        /// <summary>
        /// The network interface's current status (up or down).
        /// </summary>
        public string Status {
            get { return _status; }
        }
		
        /// <summary>
        /// A list of DNS Server entries associated with this interface.
        /// </summary>
        public Collection<string> DnsServers {
            get { return _dnsServers; }
        }
		
        /// <summary>
        /// A list of gateway addresses associated with this interface.
        /// </summary>
        public Collection<string> Gateways {
            get { return _gateways; }
        }
		
        /// <summary>
        /// This computer's hostname.
        /// </summary>
        public string Hostname {
            get { return _hostname; }
        }
		
        /// <summary>
        /// The network interface's IPv4 address (if available).
        /// </summary>
        public string IPAddressV4 {
            get { return _ipAddressV4; }
        }

        /// <summary>
        /// The network interface's IPv6 address (if available).
        /// </summary>
        public string IPAddressV6 {
            get { return _ipAddressV6; }
        }
		
        /// <summary>
        /// Gets a list of adapters and populates their information.
        /// Currently this is one of the ways to instantiate ipInfo classes.
        /// </summary>
        /// <returns>An ipInfo list containing information for all network adapters.</returns>
        public static Collection<IPInfo> GetAdapterInfoList()
        {
            Collection<IPInfo> adapterList = new Collection<IPInfo>();
            IPInterfaceProperties ipProps = null;
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			
            if (networkInterfaces.Length > 0)
            {
                foreach (NetworkInterface networkInterface in networkInterfaces)
                {
                    string ipv4 = "";
                    string ipv6 = "";
                    Collection<string> dns = new Collection<string>();
                    Collection<string> gw = new Collection<string>();
                    ipProps = networkInterface.GetIPProperties();
					
                    // Need to determine which version of the IP fits
                    for (int i = 0; i < ipProps.UnicastAddresses.Count; ++i ) 
                    {
                        switch (ipProps.UnicastAddresses[i].Address.AddressFamily) {
                            case System.Net.Sockets.AddressFamily.InterNetwork:
                                ipv4 = ipProps.UnicastAddresses[i].Address.ToString();
                                break;
								
                            case System.Net.Sockets.AddressFamily.InterNetworkV6:
                                ipv6 = ipProps.UnicastAddresses[i].Address.ToString();
                                break;
                        }
                    }
					
                    // Gather DNS servers
                    for (int i = 0; i < ipProps.DnsAddresses.Count; ++i) 
                    {
                        dns.Add(ipProps.DnsAddresses[i].ToString());
                    }
					
                    // Gather gateways
                    for (int i = 0; i < ipProps.GatewayAddresses.Count; ++i) {
                        gw.Add(ipProps.GatewayAddresses[i].Address.ToString());
                    }
					
                    adapterList.Add(new IPInfo(networkInterface.Name, ipv4, ipv6, dns, gw, Dns.GetHostName(), networkInterface.OperationalStatus.ToString()));
                }
            }			
            return adapterList;
        }
		
        /// <summary>
        /// Gets a list of IP's currently assigned to this computer.
        /// </summary>
        /// <returns>A string list containing all IP's assigned to this computer (IPv4 and IPv6).</returns>
        public static Collection<string> GetIPList()
        {
            Collection<string> ipList = new Collection<string>();
            IPAddress[] addr = Dns.GetHostAddresses(Dns.GetHostName());
            for (int i = 0; i < addr.Length; ++i) 
            {
                ipList.Add(addr[i].ToString());
            }
            return ipList;
        }
    }
}