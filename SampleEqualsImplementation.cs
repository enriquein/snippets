       public virtual bool Equals(Employee other)
        {
            if ((object)other == null)
                return false;

            return this.Id == other.Id;
        }
 
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Employee);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override int GetHashCode()
        {
            return this.Id ^ Name.GetHashCode();
        }

        public static bool operator ==(Employee A, Employee B)
        {
            if (ReferenceEquals(A, B))
                return true;

            if ((object)A == null || (object)B == null)
                return false;

            return A.Equals(B);
        }

        public static bool operator !=(Employee A, Employee B)
        {
            return !(A == B);
        }
    }