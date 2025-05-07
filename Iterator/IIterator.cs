using Domain.Widget;

namespace Iterator
{
    public interface IIterator<out T>
    {
        T Current { get; }
        bool MoveNext();
        void Reset();
    }

    public interface IIteratorAggregate<out T>
    {
        IIterator<T> GetIterator();
    }

    public class Project : IIteratorAggregate<WidgetBase>
    {
        public List<WidgetBase> Column1 { get; private set; } = new();
        public List<WidgetBase> Column2 { get; private set; } = new();
        public List<WidgetBase> Column3 { get; private set; } = new();

        public string Name { get; private set; }

        public Project(string name)
        {
            Name = name;
        }

        public void AddToColumn1(WidgetBase widget) => Column1.Add(widget);
        public void AddToColumn2(WidgetBase widget) => Column2.Add(widget);
        public void AddToColumn3(WidgetBase widget) => Column3.Add(widget);

        public IIterator<WidgetBase> GetIterator()
        {
            return new ProjectIterator(this);
        }

        public IIterator<WidgetBase> GetEnumerator() => GetIterator();

        private class ProjectIterator : IIterator<WidgetBase>
        {
            private readonly Project _project;
            private int _currentColumnIndex;
            private int _currentWidgetIndex;

            public ProjectIterator(Project project)
            {
                _project = project;
                _currentColumnIndex = 0;
                _currentWidgetIndex = -1;
            }

            public WidgetBase Current
            {
                get
                {
                    return _currentColumnIndex switch
                    {
                        0 => _project.Column1[_currentWidgetIndex],
                        1 => _project.Column2[_currentWidgetIndex],
                        2 => _project.Column3[_currentWidgetIndex],
                        _ => throw new InvalidOperationException("Invalid column index")
                    };
                }
            }

            public bool MoveNext()
            {
                _currentWidgetIndex++;

                while (_currentColumnIndex < 3)
                {
                    var currentColumn = GetCurrentColumn();
                    if (_currentWidgetIndex < currentColumn.Count)
                    {
                        return true;
                    }

                    _currentColumnIndex++;
                    _currentWidgetIndex = 0;
                }

                return false;
            }

            public void Reset()
            {
                _currentColumnIndex = 0;
                _currentWidgetIndex = -1;
            }

            private List<WidgetBase> GetCurrentColumn()
            {
                return _currentColumnIndex switch
                {
                    0 => _project.Column1,
                    1 => _project.Column2,
                    2 => _project.Column3,
                    _ => throw new InvalidOperationException("Invalid column index")
                };
            }
        }
    }
}
