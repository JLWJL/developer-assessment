import { render, screen, fireEvent } from '@testing-library/react';
import {AddTodo} from './AddTodo';

describe('AddTodo', () => {
  it('should render AddTodo component', () => {
    render(<AddTodo />);

    expect(screen.getByRole('heading', { name: 'Add Item'})).toBeInTheDocument();
    expect(screen.getByPlaceholderText('Enter description...')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Add Item' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Clear' })).toBeInTheDocument();
  });


  it('should update the input value when typing', () => {
    render(<AddTodo addTodo={jest.fn()} />);
    const input = screen.getByPlaceholderText('Enter description...');

    fireEvent.change(input, { target: { value: 'Test todo' } });

    expect(input.value).toBe('Test todo');
  });

  it('should call onClick when clicking Add Item button', () => {
    const mockOnClick = jest.fn();
    render(<AddTodo addTodo={mockOnClick} />);
    const button = screen.getByRole('button', { name: 'Add Item' });
    const input = screen.getByPlaceholderText('Enter description...');

    fireEvent.change(input, { target: { value: 'Test todo' } });
    fireEvent.click(button);

    expect(mockOnClick).toHaveBeenCalledWith({ description: 'Test todo', isComplete: false });
  });

  it('should clear the input value when clicking Clear button', () => {
    render(<AddTodo addTodo={jest.fn()} />);
    const button = screen.getByText('Clear');
    const input = screen.getByPlaceholderText('Enter description...');

    fireEvent.change(input, { target: { value: 'Test todo' } });
    fireEvent.click(button);

    expect(input.value).toBe('');
  });
});
