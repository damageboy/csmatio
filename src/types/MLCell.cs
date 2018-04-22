using System;
using System.Collections;
using System.Text;

namespace csmatio.types
{
	/// <summary>
	/// This class represents an Matlab Char array (matrix)
	/// </summary>
	/// <author>David Zier (david.zier@gmail.com)</author>
	public class MLCell : MLArray
	{
		readonly ArrayList _cells;

		/// <summary>
		/// Create a basic empty <c>MLCell</c> object for a specific set of dimensions
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		public MLCell( string Name, int[] Dims ) :
			this( Name, Dims, mxCELL_CLASS, 0 ) {}

		/// <summary>
		/// Normally this constructor is used only by <c>MatFileReader</c> and <c>MatFileWriter</c>
		/// </summary>
		/// <param name="Name">Array name</param>
		/// <param name="Dims">Array dimensions</param>
		/// <param name="Type">Array type: here <c>mxCELL_CLASS</c></param>
		/// <param name="Attributes">Array flags</param>
		public MLCell( String Name, int[] Dims, int Type, int Attributes ) :
			base( Name, Dims, Type, Attributes )
		{
			_cells = new ArrayList(M*N);

			for( var i = 0; i < M*N; i++ )
			{
				_cells.Add( new MLEmptyArray() );
			}
		}

		/// <summary>
		/// Public accessor to the cell array for 2-dimensions.
		/// </summary>
		public MLArray this[ int m, int n ]
		{
			set => _cells[ GetIndex(m,n) ] = value;
			get => (MLArray)_cells[ GetIndex(m,n) ];
		}

		/// <summary>
		/// Public accessor to the cell array for 1 dimension.
		/// </summary>
		public MLArray this[ int Index ]
		{
			set => _cells[ Index ] = value;
			get => (MLArray)_cells[ Index ];
		}

		/// <summary>
		/// Public Get Accessor for the cells array list
		/// </summary>
		public ArrayList Cells => _cells;

		/// <summary>
		/// Get a string representation for the content of the array.
		/// </summary>
		/// <returns>A string representation.</returns>
		public override string ContentToString()
		{
			var sb = new StringBuilder();
			sb.Append( Name + " = \n" );

			for( var m = 0; m < M; m++ )
			{
				sb.Append("\t");
				for( var n = 0; n < N; n++ )
				{
					sb.Append( this[m,n].ContentToString() );
					sb.Append("\t");
				}
				sb.Append("\n");
			}
			return sb.ToString();
		}


	}
}
