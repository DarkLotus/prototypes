/**
 * 
 */
package com.mythiksoftware.ProjectFrame;

import Managers.PersistenceManager;

import com.artemis.Entity;
import com.artemis.World;
import com.artemis.managers.GroupManager;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;

import components.MapComponent;
import components.PlayerComponent;
import components.SpriteComponent;
import components.UIButtonComponent;
import components.VelocityComponent;
import components.WorldPositionComponent;

/**
 * @author James
 *
 */
public class EntityFactory {

	
	
	public static Entity createMap(World world, String name)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new MapComponent(name));
		return entity;
	}
	public static Entity createButton(World world, String label,Vector2 loc, ChangeListener handler)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new UIButtonComponent(label,loc,handler));
		return entity;
	}
	public static Entity createObject(World world, String name)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new WorldPositionComponent(5,5));
		entity.addComponent(new SpriteComponent(5));
		world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}
	public static Entity createPlayer(World world, String name)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new WorldPositionComponent(5,5));
		entity.addComponent(new VelocityComponent());
		entity.addComponent(new PlayerComponent());
		entity.addComponent(new SpriteComponent(5));
		world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}
}
