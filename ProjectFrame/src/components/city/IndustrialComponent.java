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

	/**
	 * @param i
	 * @param j
	 * @param k
	 */
	public IndustrialComponent(int i, int j, int k) {
		Score = i;
		Life = j;
		Money = k;
	}
	public int Score;
	public int Life;
	public int Money;
	
	public IndustrialComponent()
	{}
}
