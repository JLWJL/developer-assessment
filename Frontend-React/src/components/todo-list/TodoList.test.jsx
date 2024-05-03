import React from 'react';
import { render, fireEvent, screen } from '@testing-library/react';
import { ToDoList } from './TodoList';

describe('ToDoList', () => {
  const todos = [
    { id: 1, description: 'Task 1', isCompleted: false },
    { id: 2, description: 'Task 2', isCompleted: true }
  ];

  const getItemsMock = jest.fn();
  const markCompleteMock = jest.fn();

  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('should render ToDoList component with correct number of items', () => {
    render(<ToDoList todos={todos} getItems={getItemsMock} markComplete={markCompleteMock} />);

    expect(screen.getByText('Showing 2 Item(s)')).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Refresh' })).toBeInTheDocument();
    expect(screen.getByText('Task 1')).toBeInTheDocument();
    expect(screen.getByText('Task 2')).toBeInTheDocument();
    expect(screen.getAllByRole('button', {name: /mark as/i})).toHaveLength(2); // 2 mark complete buttons + 1 refresh button
  });

  it('should call getItems function when Refresh button is clicked', () => {
    render(<ToDoList todos={todos} getItems={getItemsMock} markComplete={markCompleteMock} />);
    const refreshButton = screen.getByRole('button', { name: 'Refresh' });

    fireEvent.click(refreshButton);

    expect(getItemsMock).toHaveBeenCalledTimes(1);
  });

  it('should call markComplete function with correct todo when Mark Complete button is clicked', () => {
    render(<ToDoList todos={todos} getItems={getItemsMock} markComplete={markCompleteMock} />);
    const markCompleteButton = screen.getByTestId('toggle-complete-1');

    fireEvent.click(markCompleteButton);

    expect(markCompleteMock).toHaveBeenCalledWith(todos[0]);
  });
});
