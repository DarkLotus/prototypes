/**
 * 
 */
package components.city;

import com.artemis.Component;

/**
 * @author James
 * Stores player data, Score,Lives,Money etc
 */
public class IndustrialComponent extends Component {


	// Comercial system adds residents to employees
	public int Employees;
	public int MaxEmployees = 100;

	//Which system, checks for nearby shops, adds a customer? maybe residential as part of the people cycle?

	public int Customers = 0;

	public int Tax;

	public IndustrialComponent()
	{}
}
