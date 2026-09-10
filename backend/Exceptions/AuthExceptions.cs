namespace backend.exceptions;

public class InvalidCredentialsException(string message) : DomainExceptions(message) { }

public class EmailAlreadyExistsException(string message) : DomainExceptions(message) { }

public class UsernameAlreadyExistsException(string message) : DomainExceptions(message) { }

public class AuthRoleNotFoundException(string message) : DomainExceptions(message) { }

public class AdminRegistrationException(string message) : DomainExceptions(message) { }

public class UserDeactivatedException(string message) : DomainExceptions(message) { }

public class EmailNotFoundException(string message) : DomainExceptions(message) { }

public class ResourceNotFoundException(string message) : DomainExceptions(message) { }