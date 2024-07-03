using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApi.Entities;

public enum UserStatus
{
    [EnumMember(Value = "I")]
    InActive,
    [EnumMember(Value = "A")]
    Active,
    [EnumMember(Value = "T")]
    Terminated

}