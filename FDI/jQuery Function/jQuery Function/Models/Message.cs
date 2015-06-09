using System;

namespace FDI.Utils
{
    [Serializable]
    public class JsonMessage
    {
        #region các biến
        private bool _erros;
        private string _message;
        private string _ID;
        private int _Type;
        #endregion

        #region Thuộc tính
        public bool Erros
        {
            get { return _erros; }
            set { _erros = value; }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        #endregion

        #region Constructer
        public JsonMessage()
        {
            _erros = false;
            _message = string.Empty;
            _ID = string.Empty;
            _Type = 0;
        }

        public JsonMessage(bool erros, string message)
        {
            _erros = erros;
            _message = message;
        }
        #endregion

    }
}
