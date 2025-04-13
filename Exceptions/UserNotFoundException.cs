namespace LibraryManagement.Exceptions;

public class UserNotFoundException(string message) : LibraryException(message);