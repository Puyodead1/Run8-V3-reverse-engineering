import typing, abc

class IIdentity(typing.Protocol):
    @property
    def AuthenticationType(self) -> str: ...
    @property
    def IsAuthenticated(self) -> bool: ...
    @property
    def Name(self) -> str: ...


class IPrincipal(typing.Protocol):
    @property
    def Identity(self) -> IIdentity: ...
    @abc.abstractmethod
    def IsInRole(self, role: str) -> bool: ...


class PrincipalPolicy(typing.SupportsInt):
    @typing.overload
    def __init__(self, value : int) -> None: ...
    @typing.overload
    def __init__(self, value : int, force_if_true: bool) -> None: ...
    def __int__(self) -> int: ...
    
    # Values:
    UnauthenticatedPrincipal : PrincipalPolicy # 0
    NoPrincipal : PrincipalPolicy # 1
    WindowsPrincipal : PrincipalPolicy # 2


class TokenImpersonationLevel(typing.SupportsInt):
    @typing.overload
    def __init__(self, value : int) -> None: ...
    @typing.overload
    def __init__(self, value : int, force_if_true: bool) -> None: ...
    def __int__(self) -> int: ...
    
    # Values:
    None_ : TokenImpersonationLevel # 0
    Anonymous : TokenImpersonationLevel # 1
    Identification : TokenImpersonationLevel # 2
    Impersonation : TokenImpersonationLevel # 3
    Delegation : TokenImpersonationLevel # 4

