import React, { useEffect, useState } from 'react';

const Profile = () => {
  const [profile, setProfile] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch('/api/ProfileApi/GetProfile')
      .then((response) => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then((data) => {
        setProfile(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching profile:', error);
        setLoading(false);
      });
  }, []);

   // Convert the BLOB data to a base64 image source
   const convertBlobToBase64 = (blob) => {
    return `data:image/jpeg;base64,${blob}`;
  };

  if (loading) {
    return <p>Loading profile...</p>;
  }

  return (
    <div id="profile-root">
      {profile ? (
        <div>
          <h2>Hello, {profile.userName}!</h2>
          {/* <p>Email: {profile.email}</p> */}
          <img 
          src={convertBlobToBase64(profile.profilePicture)} 
          alt={`${profile.userName}'s profile`} 
          style={{ width: '300px', height: '300px', borderRadius: '5%' }}
        />
        </div>
      ) : (
        <p>Profile data not found.</p>
      )}
    </div>

  );
};

export default Profile;
