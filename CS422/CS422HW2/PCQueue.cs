using System;

namespace CS422
{
	public class PCQueue
	{
		private Node mHead;
		private Node mTail;

		public PCQueue ()
		{
			mHead = null;
		}

		public void Enqueue(int item)
		{
			if (mHead == null) {
				mHead = new Node (item, null);
			}
			else if (mTail == null) {
				mTail = new Node (item, null);
				mHead.Next = mTail;
			} else {
				mTail.Next = new Node (item, null);
				mTail = mTail.Next;
			} 
		}

		public bool Dequeue(ref int  out_value)
		{
			if (mHead == null)
				return false;

			out_value = mHead.Data;
			mHead = mHead.Next;

			return true;			 
		} 
	 
	}

	internal class Node
	{
		private Node mNext;
		private int mValue;
		public Node(int value, Node next)
		{ 
			if (next != null)
				mNext = next;
			else {
				mNext = null;
			}

			mValue = value;
		}

		public int Data 
		{
			get { return mValue; }
			set { mValue = value; }			
		} 

		public Node Next { get { return mNext; } set{ mNext = value; } }
		 
	}
		
}

