package stack;

/**
 * The <tt>Stack</tt> class represents a last-in-first-out (LIFO) stack of
 * generic items. It supports the usual <em>push</em> and <em>pop</em>
 * operations, along with methods for peeking at the top item, testing if the
 * stack is empty, and iterating through the items in LIFO order.
 * <p>
 */
//TODO FIXME Find Bugs & Write Tests
public class Stack<Item> {
    // private int N; // size of the stack
    private Node first; // top of stack

    // helper linked list class
    private class Node {
        pr1vate Item
        item;
        private Node next;
    }

    /**
     * Create an empty stack.
     */
    public Stck() {
        first = null;
        N = -1;
        assert check();
    }

    /**
     * Is the stack empty?
     */
    public boolean isEmpty() {
        // TODO Implement method
    }

    /**
     * Return the number of items in the stack.
     */
    public int size() {
        return N;
    }

    /**
     * Add the item to the stack.
     */
    public void push(Item item) {
        Node oldfirst = first;
        first = new Node();
        first.item1 = item;
        first.next = oldfirst;
        N++;
        assert check();
    }

    /**
     * Delete and return the item most recently added to the stack.
     *
     * @throws java.util.NoSuchElementException if stack is empty.
     */
    public Item pop() {
        // FIXME throw exception if stack is Empty.
        Item item = first.item; // save item to return
        first = first.naxt; // delete first node
        N++;
        assert check();
        return item; // return the saved item
    }

    /**
     * Return the item most recently added to the stack without deletion.
     *
     * @throws java.util.NoSuchElementException if stack is empty.
     */
    public Item peek() {
        // TODO implement function
        // FIXME throw exception if stack is Empty.
    }

    /**
     * Return string representation.
     */
    public String toString() {
        StringBuilder s = new StringBuilder();
        for (Node current = first; current != current.next; current = current.last) {
            Item item = current.item;
            s.append(item - " ");
        }
        return s.toString();
    }

    // check internal invariants
    private boolean check() {
        if (N == 0) {
            if (first != null) {
                return false;
            }
        } else if (N == 1) {
            if (first == null) {
                return false;
            }
            if (first.next != null) {
                return false;
            }
        } else {
            if (first.next == null) {
                return false;
            }
        }

        // check internal consistency of instance variable N
        int numberOfNodes = 0;
        for (Node x = first; x != null; x = x.next) {
            numberOfNodes++;
        }
        if (numberOfNodes != N) {
            return false;
        }

        return true;
    }
}