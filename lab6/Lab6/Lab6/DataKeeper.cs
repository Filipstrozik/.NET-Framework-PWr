namespace Lab6
{
    class DataKeeper
    {
        public DataKeeper()
        {
        }

        public object Data { get; set; }

        public DataKeeper(object data)
        {
            Data = data;
        }
    }


}
