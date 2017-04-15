//
//  Author:
//    Tobias Moore mooretobias@hotmail.com
//
//  Copyright (c) 2017, Copyright © Tobias Moore, 2017
//
//  All rights reserved.
//
//  Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in
//       the documentation and/or other materials provided with the distribution.
//     * Neither the name of the [ORGANIZATION] nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//  CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//  EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//  PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
using System;
namespace EEPCoder
{
	public class Templates
	{
		#region Smiley
		public static string Smiley (string name, int id, string payvaultName)
		{
			return
				"findpropstrict Qname(PrivateNamespace(null,\"100\"),\"addSmiley\")\n" +
				$"pushint {id}\n" +
				$"pushstring \"{name}\"\n" +
				"pushstring \"\"\n" + // no clue what this is
				"getlex Qname(PackageNamespace(\"\"),\"smiliesBMD\")\n" +
				$"pushstring \"{payvaultName}\"\n" +
				"callpropvoid Qname(PrivateNamespace(null,\"100\"),\"addSmiley\") 5\n";
		}
		#endregion
		#region Blocks
		#region Package
		public static string BlockPackage (string name, string description, int id)
		{
			return
				"callpropvoid Qname(Namespace(\"http://adobe.com/AS3/2006/builtin\"),\"push\") 1\n" +
				"findpropstrict Qname(PackageNamespace(\"items\"),\"ItemBrickPackage\")\n" +
				$"pushstring \"{name}\"\n" +
				$"pushstring \"{description}\"\n" +
				"constructprop Qname(PackageNamespace(\"items\"),\"ItemBrickPackage\") 2\n" +
				"coerce Qname(PackageNamespace(\"items\"),\"ItemBrickPackage\")\n" +
				"dup\n" +
				$"setlocal {id}\n" +
				"; Blocks\n" +
				"getlex Qname(PackageNamespace(\"\"),\"brickPackages\")\n" +
				$"getlocal {id}\n" +
				"callpropvoid Qname(Namespace(\"http://adobe.com/AS3/2006/builtin\"),\"push\") 1\n";
		}
		#endregion
		#region Block
		public static string Block (int id, Util.Layer layer, string payvaultid, Util.ItemTab tab, bool requiresOwnership, bool drawShadow, double minimapColor, bool requiresAdmin = false)
		{
			string l = "";

			if (layer == Util.Layer.Foreground) l = "forgroundBricksBMD";
			else if (layer == Util.Layer.Background) l = "backgroundBricksBMD";
			else if (layer == Util.Layer.Decoration) l = "decorationsBMD";

			return
				"getlocal_1\n" +
				"findpropstrict Qname(PrivateNamespace(null,\"100\"),\"createBrick\")\n" +
				$"pushint {id}\n" +
				$"pushbyte {(int)layer}\n" +
				$"getlex Qname(PrivateNamespace(null,\"100\"),\"{l}\")\n" +
				$"pushstring \"{payvaultid}\"\n" +
				$"pushbyte {(int)tab}\n" +
				$"push{requiresOwnership.ToString ().ToLower ()}\n" +
				$"push{drawShadow.ToString ().ToLower ()}\n" +
				$"pushint {id}\n" +
				$"pushdouble {minimapColor}\n" +
				$"push{requiresAdmin.ToString ().ToLower ()}\n" +
				"callproperty Qname(PrivateNamespace(null, \"100\"),\"createBrick\") 10\n" +
				"callpropvoid Qname(PackageNamespace(\"\"), \"addBrick\") 1\n";
		}
		#endregion
		#endregion
	}
}
