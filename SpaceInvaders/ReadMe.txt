Team: Shadowfax

Req:
Player:
	movement: 
		get input with GetAxisRaw("Horizontal") and moves with changing the rigidbody's velocity.
		fixes the player's movements with walls (with rigidbodys and colliders)
	shooting:
		there is a bulletExists boolean, if there is no bullet then:
			gets input with GetKeyDown and instantiate a bullet at the current location and sets bulletExists to true.
		once the player's bullet collides with something it invokes an event which the playerController script listens for and changes bulletExist back to false.		 
		
	death:
		in the level controller it controls the lives the players have, 
		when the bullet hits the player, it calls the levelManager's singleton and decreases life and checks if life == 0. Upon reaching zero the levelManager activates the startmenu

Enemy:
	generated:
		using a 2D array, the enemies are generated with 11 columns and 5 rows.
	movement:
		movement is done in the parent object (enemyController), if the sum of the time that has passed (aka a variable that totals Time.deltatime) is greater than a certain number, the unit moves (via changing the transform)
		there is a boolean isMovingRight to tell the object which way to move. Befor moving, the controller loops through all the enemies in the 2D array and fires off a ray cast to check if the array will bump into a wall. If it will then the direction is flipped and the formation moves down 1 unit.
		gameover will occure once one(1) enemy reaches the bottom and enters a trigger which calls upon the level manager singleton and activates the game over screen/state.
	shooting:
		given a random float interval of time, if the accumulated time is greater than the random float the enemyController will pick a random enemy to shoot by:
		1) picking a random column
		2) getting the lowest row of that column where the enemy still exist (that is the position in the 2D array != null; if the column is empty the system choose another column)
		3)telling that enemy to instanciate a bullet which fires towards the player.
	death:
		when the enemies are inicialized they are inicialized with a point value.
		when a bullet collides with the enemy, (in the bullet class) it cashes the enemy's points, destroys both the bullet and enemy, and calls upon the level manger singleton to update the score. 
Gameover:
	upon the levelManager's life counter reaching 0 or when an enemy enters a special trigger, the system will call the levelManger's singleton and activate the gameOver menu.
	player's live(s) is managed by level manager *see Player death to see how lives/gameover is managed
UI:
	startmenu:
		starts out the game on a menu with start and quit.
		buttons function inside MenuController. 
	in game:
		if escape is pressed it toggles pause by setting time
	gameover:
		once the gameover conditions have been reaches the the levelManager's singleton has been called, the levelManager's singleton and sets Time.timeScale = 0 and activates gameOver menu
Win:
	upon hitting an enemy: checks if enemyController's 2D array is all null. If it is, calles levelManager's singleton and sets Time.timeScale = 0 and activates win menu. 


Github:
https://github.com/eggshelly/161-SpaceInvaders-2018

Note; due to scheduling conflicts in our team we made our own versions seperately and put them together in the end. 
We could not meet during the process of making the game.