import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Profile = () => {
  const [profile, setProfile] = useState(null);

  useEffect(() => {
    axios.get('/User/Profile')
      .then(response => setProfile(response.data))
      .catch(error => console.error(error));
  }, []);

  if (!profile) return <div>Loading...</div>;

return (
  <div className="profile">
    <h1>{profile.UserName}</h1>
    <p>Email: {profile.Email}</p>
    {profile.ProfilePicture && (
      <img src="data:image/jpeg;base64,@Convert.ToBase64String(Model.Game.GamePicture)" alt="Game Picture" style="max-width: 400px;" />
    )}
  </div>
);
}

export default Profile;
