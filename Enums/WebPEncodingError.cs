namespace WebP.Net.Enums
{
    public enum WebPEncodingError
    {
        Ok,
        OutOfMemory,
        BitstreamOutOfMemory,
        NullParameter,
        InvalidConfiguration,
        BadDimension,
        Partition0Overflow,
        PartitionOverflow,
        BadWrite,
        FileTooBig,
        UserAbort,
        Last,
    }
}