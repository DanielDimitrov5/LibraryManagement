namespace LibraryManagement.Exceptions;

public class BookNotFoundException(string message) : LibraryException(message);