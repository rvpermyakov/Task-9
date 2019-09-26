using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
	class Node
	{
		public int value;
		public Node next = null;
		public Node previous = null;

		public Node(Node previous, Node next, int value)
		{
			this.previous = previous;
			this.next = next;
			this.value = value;
		}

		public Node() { }
	}

	class LinkedList
	{
		public Node head = null;

		public LinkedList() { }

		public void Add(int value) {
			if (head == null) {
				head = new Node(head, head, value);
				head.next = head.previous = head;
			} else
				Add(value, head);
		}

		public void Add(int value, Node element) {
			if (element.next == head) {
				if (element.value < value) {
					Node insert = new Node(element, element.next, value);
					element.next = insert;
					insert.next.previous = insert;
					return;
				}
			}

			if (element.value >= value) {
				Node insert = new Node(element.previous, element, value);
				element.previous = insert;
				insert.previous.next = insert;
				if (element == head)
					head = insert;
				return;
			}

			Add(value, element.next);
		}

		public Node Find(int value, Node element)
		{
			if (head == null)
				return null;

			if (element.value == value)
				return element;

			if (element.next == head)
				return null;

			return Find(value, element.next);
		}

		public bool Delete(int value, Node element)
		{
			if (head == null)
				return false;

			if (element.value == value) {
				if (element == head && element.next == head)
					head = null;
				else {
					if (element == head)
						head = element.next;

					element.next.previous = element.previous;
					element.previous.next = element.next;
				}
				return true;
			}

			if (element.next == head)
				return false;

			return Delete(value, element.next);
		}

		public void Print(Node element)
		{
			if (head == null)
				return;

			Console.Write("{0} ", element.value);
			if (element.next == head) {
				Console.WriteLine();
				return;
			}

			Print(element.next);
		}
	}
}

namespace Linked_List
{
	class Program
	{
		static void Main(string[] args)
		{
			int n = Convert.ToInt32(Console.ReadLine());
			Structures.LinkedList list = new Structures.LinkedList();
			for (int i = 1; i <= n; i++)
				list.Add(i);
			list.Print(list.head);

			string console;
			Console.Write("command (написать info): ");
			while ((console = Console.ReadLine()) != "сделано") {
				string[] temp = console.Split(' ');
				switch (temp[0]) {
					case "info": {
							Console.WriteLine("find <число> - true - если есть в списке, false - если нет");
							Console.WriteLine("delete <число> - удаляет элемент. Если элемент не найден, выведет соответсвующее сообщение .");
							Console.WriteLine("print - выводит список");
						}
						break;

					case "find": {
							if (list.Find(Convert.ToInt32(temp[1]), list.head) == null)
								Console.WriteLine("False");
							else
								Console.WriteLine("True");
						}
						break;

					case "delete": {
							if (list.Delete(Convert.ToInt32(temp[1]), list.head))
								Console.WriteLine("Элемент удален");
							else
								Console.WriteLine("Элемент не найден");
						}
						break;

					case "print": {
							list.Print(list.head);
						}
						break;
				}
				Console.Write("command: ");
			}

		}
	}
}