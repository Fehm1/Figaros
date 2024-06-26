﻿using Figaros.Shared.Utilities.Results.ComplexTypes;

namespace Figaros.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
}
