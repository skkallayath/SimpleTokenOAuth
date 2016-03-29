namespace SimpleTokenOAuth.Config
{
    using System.Configuration;
    using System.Web.Configuration;

    /// <summary>
    /// The Simple Token Auth Configuration
    /// </summary>
    public class SimpleTokenAuthConfiguration : ConfigurationSection
    {
        /// <summary>
        /// The private instance
        /// </summary>
        private static SimpleTokenAuthConfiguration _instance;

        /// <summary>
        /// The Singleton Instance
        /// </summary>
        public static SimpleTokenAuthConfiguration Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (SimpleTokenAuthConfiguration)WebConfigurationManager.GetSection("simpleTokenAuth");
                }
                return _instance;
            }
        }

        /// <summary>
        /// The Is Enabled
        /// </summary>
        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool IsEnabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        /// <summary>
        /// The Token List
        /// </summary>
        [ConfigurationProperty("Tokens", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(TokenCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public TokenCollection Tokens
        {
            get
            {
                return (TokenCollection)base["Tokens"];
            }
        }
    }

    /// <summary>
    /// The Token Collection
    /// </summary>
    public class TokenCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        public TokenCollection()
        {

        }

        /// <summary>
        /// The Indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TokenConfig this[int index]
        {
            get { return (TokenConfig)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// The add
        /// </summary>
        /// <param name="tokenConfig"></param>
        public void Add(TokenConfig tokenConfig)
        {
            BaseAdd(tokenConfig);
        }

        /// <summary>
        /// The Clear
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        /// <summary>
        /// The CreateNewElemnt
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TokenConfig();
        }

        /// <summary>
        /// The GetElementKey
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TokenConfig)element).Token;
        }

        /// <summary>
        /// The Remove
        /// </summary>
        /// <param name="serviceConfig"></param>
        public void Remove(TokenConfig serviceConfig)
        {
            BaseRemove(serviceConfig.Token);
        }

        /// <summary>
        /// The Remove At
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// The Remove
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            BaseRemove(key);
        }

        /// <summary>
        /// The GetElementByKey
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TokenConfig GetElement(object key)
        {
            return BaseGet(key) as TokenConfig;
        }
    }

    /// <summary>
    /// The tokenConfig
    /// </summary>
    public class TokenConfig : ConfigurationElement
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        public TokenConfig() { }

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="token"></param>
        /// <param name="allow"></param>
        public TokenConfig(string token, string info, bool allow)
        {
            Info = info;
            Token = token;
            IsAllowed = allow;
        }

        /// <summary>
        /// The Token
        /// </summary>
        [ConfigurationProperty("token", IsRequired = true, IsKey = true)]
        public string Token
        {
            get { return (string)this["token"]; }
            set { this["token"] = value; }
        }

        /// <summary>
        /// The Info
        /// </summary>
        [ConfigurationProperty("info", IsRequired = false, IsKey = false)]
        public string Info
        {
            get { return (string)this["info"]; }
            set { this["info"] = value; }
        }

        /// <summary>
        /// The IsAllowed
        /// </summary>
        [ConfigurationProperty("allow", DefaultValue = true, IsRequired = false, IsKey = false)]
        public bool IsAllowed
        {
            get { return (bool)this["allow"]; }
            set { this["allow"] = value; }
        }
    }
}