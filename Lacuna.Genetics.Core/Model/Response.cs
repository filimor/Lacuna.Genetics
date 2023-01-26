﻿namespace Lacuna.Genetics.Core.Model;

internal class Response
{
    public string Code { get; set; }
    public string Message { get; set; }
    public string? AccessToken { get; set; }
    public Job? Job { get; set; }
}